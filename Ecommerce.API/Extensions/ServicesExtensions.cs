using Asp.Versioning;
using Ecommerce.API.Contracts.Requests.Products;
using Ecommerce.API.DataEF.Context;
using Ecommerce.API.DataEF.IRepositories;
using Ecommerce.API.DataEF.Repositories;
using Ecommerce.API.Mapping.ProductProfile;
using Ecommerce.API.Services.IServices;
using Ecommerce.API.Services.Products;
using Ecommerce.API.Validators.Attributes;
using Ecommerce.API.Validators.Products;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Ecommerce.API.Extensions
{
    /// <summary>
    /// Init app services extensions
    /// </summary>
    public static class ServicesExtensions
    {
        /// <summary>
        /// Services configuration
        /// </summary>
        public static IServiceCollection InitServices(this IServiceCollection services, ConfigurationManager config)
        {
            AddSwagger(services);
            AddApiVersioning(services);
            AddDbContext(services, config);
            AddAutoMapper(services);

            AddValidators(services);
            AddRepositories(services);
            AddServices(services);

            services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
            services.AddControllers(options =>
            {
                options.Filters.Add<ValidateIdAttribute>();
            });
            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
            });
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }

        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ProductProfile).Assembly);

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductsRepository>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();

            return services;
        }

        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<AddProductRequestDto>, AddProductValidator>();
            services.AddScoped<IValidator<UpdateProductRequestDto>, UpdateProductValidator>();

            return services;
        }

        private static void AddDbContext(IServiceCollection services, ConfigurationManager config)
        {
            services.AddDbContext<EcommerceContext>(optionsBuilder =>
            {
                optionsBuilder
                    .UseSqlServer(config.GetConnectionString("DefaultConnection"))
                    .LogTo(Console.WriteLine);
            },
            ServiceLifetime.Scoped,
            ServiceLifetime.Singleton);
        }

        private static void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new()
                {
                    Title = "Ecommerce.Api",
                    Version = "v1"
                });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
                options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });
        }

        private static void AddApiVersioning(IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1);
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader());
            }).AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });
        }
    }
}
