using Ecommerce.API.Contracts.Dtos.Products;
using Ecommerce.API.DataEF.Context;
using Ecommerce.API.DataEF.Converters.ProductConverter;
using Ecommerce.API.DataEF.Helpers;
using Ecommerce.API.DataEF.IRepositories;
using Ecommerce.API.DataEF.Models;
using Ecommerce.API.Domain;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.DataEF.Repositories
{
    /// <summary>
    /// Products repository
    /// </summary>
    /// <param name="context"></param>
    public class ProductsRepository(EcommerceContext context) : IProductRepository
    {
        private readonly EcommerceContext _context = context;

        /// <inheritdoc />
        public async Task<int> AddAsync(CreateProductDto product)
        {
            ProductModel createdProduct = new()
            {
                Title = product.Title,
                Description = product.Description,
                Price = product.Price
            };

            await _context.Products.AddAsync(createdProduct);

            int affectedRows = await _context.SaveChangesAsync();

            return affectedRows > 0 ? createdProduct.Id : 0;
        }

        /// <inheritdoc />
        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products
                .Where(p => p.Id == id)
                .Select(ProductConverter.ToDomain())
                .FirstOrDefaultAsync();
        }

        /// <inheritdoc />
        public async Task<bool> UpdateAsync(int id, Product product)
        {
            var affectedRows = await _context.Products.Where(p => p.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(b => b.Title, product.Title)
                    .SetProperty(b => b.Description, product.Description)
                    .SetProperty(b => b.Price, product.Price));

            return affectedRows > 0;
        }

        /// <inheritdoc />
        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Products.AnyAsync(p => p.Id == id);
        }

        /// <inheritdoc />
        public async Task<bool> DeleteAsync(int id)
        {
            var affectedRows = await _context.Products
              .Where(p => p.Id == id)
              .ExecuteDeleteAsync();

            return affectedRows > 0;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Product>> GetAllAsync(GetListOfProductsDto request)
        {
            var query = _context.Products.AsQueryable();

            var selectedQuery = query.Select(ProductConverter.ToDomain());

            selectedQuery = GetFiltered(selectedQuery, request);
            selectedQuery = GetSorted(selectedQuery, request);

            selectedQuery = selectedQuery.Skip((request.Page - 1) * (request.PageSize))
                         .Take(request.PageSize);

            return await selectedQuery.AsNoTracking().ToListAsync();
        }

        private static IQueryable<Product> GetFiltered(IQueryable<Product> query,
            GetListOfProductsDto request)
        {
            if (request.MinPrice.HasValue)
            {
                query = query.Where(p => p.Price >= request.MinPrice.Value);
            }

            if (request.MaxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= request.MaxPrice.Value);
            }

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                query = query.Where(p => p.Title.Contains(request.Search));
            }

            return query;
        }

        private static IQueryable<Product> GetSorted(IQueryable<Product> query,
            GetListOfProductsDto request)
        {
            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                if (request.SortOrder?.ToLower() == "desc")
                {
                    query = query.OrderByDescending(EfHelpers.SortHelper<Product>(request.SortBy));
                }
                else
                {
                    query = query.OrderBy(EfHelpers.SortHelper<Product>(request.SortBy));
                }
            }
            else
            {
                query = query.OrderBy(p => p.Title);
            }

            return query;
        }
    }
}
