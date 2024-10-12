using AutoMapper;
using Ecommerce.API.Contracts.Dtos.Products;
using Ecommerce.API.Contracts.Requests.Products;
using Ecommerce.API.Contracts.Responses.Common;
using Ecommerce.API.Contracts.Responses.Products;
using Ecommerce.API.DataEF.IRepositories;
using Ecommerce.API.Domain;
using Ecommerce.API.Exceptions.Products;
using Ecommerce.API.Mapping;
using Ecommerce.API.Services.IServices;

namespace Ecommerce.API.Services.Products
{
    /// <summary>
    /// Product management service
    /// </summary>
    /// <param name="productRepository"></param>
    /// <param name="mapper"></param>
    public class ProductService(
        IProductRepository productRepository,
        IMapper mapper) : IProductService
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IMapper _mapper = mapper;

        /// <inheritdoc />
        public async Task<int> AddProductAsync(AddProductRequestDto product)
        {
            int productId = await _productRepository.AddAsync(new CreateProductDto
            {
                Title = product.Title,
                Description = product.Description,
                Price = product.Price
            });

            return productId;
        }

        /// <inheritdoc />
        public async Task<GetProductResponseDto> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            return product == null
                ? throw new ProductNotFoundException(id)
                : product.MapToGetByIdResponse();
        }

        /// <inheritdoc />
        public async Task<bool> UpdateProductAsync(int id, UpdateProductRequestDto product)
        {
            if (!await _productRepository.ExistsAsync(id))
            {
                throw new ProductNotFoundException(id);
            }

            Product productForUpdate = new()
            {
                Title = product.Title,
                Description = product.Description ?? string.Empty,
                Price = product.Price
            };

            return await _productRepository.UpdateAsync(id, productForUpdate);
        }

        /// <inheritdoc />
        public async Task<bool> DeleteProductAsync(int id)
        {
            if (!await _productRepository.ExistsAsync(id))
            {
                throw new ProductNotFoundException(id);
            }

            return await _productRepository.DeleteAsync(id);
        }

        public async Task<PaginatedListResponseDto<GetProductResponseDto>> GetAllProductsAsync(GetListOfProductsRequestDto query)
        {
            var options = _mapper.Map<GetListOfProductsDto>(query);

            var products = await _productRepository.GetAllAsync(options);

            var mappedProducts = _mapper.Map<IEnumerable<GetProductResponseDto>>(products);

            return new PaginatedListResponseDto<GetProductResponseDto>
            {
                Results = mappedProducts
            };
        }
    }
}
