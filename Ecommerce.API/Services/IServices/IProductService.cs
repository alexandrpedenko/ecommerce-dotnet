using Ecommerce.API.Contracts.Requests.Products;
using Ecommerce.API.Contracts.Responses.Common;
using Ecommerce.API.Contracts.Responses.Products;
using Ecommerce.API.Exceptions.Common;

namespace Ecommerce.API.Services.IServices
{
    /// <summary>
    /// Product service interface
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Create product service method
        /// </summary>
        /// <param name="product"></param>
        /// <returns>Product Id</returns>
        /// <exception cref="InternalServerErrorException"></exception>
        Task<int> AddProductAsync(AddProductRequestDto product);

        /// <summary>
        /// Retrieves a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product.</param>
        /// <returns>The product with the specified ID.</returns>
        /// <exception cref="NotFoundException"></exception>
        Task<GetProductResponseDto> GetProductByIdAsync(int id);

        /// <summary>
        /// Updates an existing product in the repository.
        /// </summary>
        /// <param name="id">Id to find the product.</param>
        /// <param name="product">The product with updated data.</param>
        /// <returns>The boolean value means whether the product was updated</returns>
        /// <exception cref="NotFoundException"></exception>
        /// <exception cref="InternalServerErrorException"></exception>
        Task<bool> UpdateProductAsync(int id, UpdateProductRequestDto product);

        /// <summary>
        /// Deletes an existing product in the repository.
        /// </summary>
        /// <param name="id">Id to find the product.</param>
        /// <returns>The boolean value means whether the product was deleted</returns>
        /// <exception cref="NotFoundException"></exception>
        /// <exception cref="InternalServerErrorException"></exception>
        Task<bool> DeleteProductAsync(int id);

        /// <summary>
        /// Retrieves all products.
        /// </summary>
        /// <param name="query">Filtering and sorting options.</param>
        /// <returns>A list of products.</returns>
        Task<PaginatedListResponseDto<GetProductResponseDto>> GetAllProductsAsync(GetListOfProductsRequestDto query);
    }
}
