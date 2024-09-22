using AutoMapper;
using Ecommerce.API.Abstraction.IRepositories;
using Ecommerce.API.Abstraction.IServices;
using Ecommerce.API.Contracts.Dtos.Products;
using Ecommerce.API.Contracts.Requests.Products;
using Ecommerce.API.Contracts.Responses.Common;
using Ecommerce.API.Contracts.Responses.Products;
using Ecommerce.API.Domain;
using Ecommerce.API.Exceptions.Common;
using Ecommerce.API.Exceptions.Products;
using Ecommerce.API.Mapping;

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

        private readonly string errorMessage = "Error during adding product occurred";

        /// <inheritdoc />
        public async Task<int> AddProductAsync(AddProductRequestDto product)
        {
            int productId = await _productRepository.AddProductAsync(new CreateProductDto
            {
                Title = product.Title,
                Description = product.Description,
                Price = product.Price
            });

            return productId > 0
                ? productId
                : throw new InternalServerErrorException(errorMessage);
        }

        /// <inheritdoc />
        public async Task<GetProductResponseDto> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);

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

            var isUpdated = await _productRepository.UpdateProductAsync(id, productForUpdate);

            return !isUpdated
                ? throw new InternalServerErrorException(errorMessage)
                : isUpdated;
        }

        /// <inheritdoc />
        public async Task<bool> DeleteProductAsync(int id)
        {
            if (!await _productRepository.ExistsAsync(id))
            {
                throw new ProductNotFoundException(id);
            }

            var isDeleted = await _productRepository.DeleteProductAsync(id);

            return !isDeleted
                ? throw new InternalServerErrorException(errorMessage)
                : isDeleted;
        }

        public async Task<PaginatedListResponseDto<GetProductResponseDto>> GetAllProductsAsync(GetListOfProductsRequestDto query)
        {
            var options = _mapper.Map<GetListOfProductsDto>(query);

            var products = await _productRepository.GetAllProductsAsync(options);

            var mappedProducts = _mapper.Map<IEnumerable<GetProductResponseDto>>(products);

            return new PaginatedListResponseDto<GetProductResponseDto>
            {
                Results = mappedProducts
            };
        }
    }
}
