using Ecommerce.API.Contracts.Dtos.Products;
using Ecommerce.API.Domain;

namespace Ecommerce.API.Abstraction.IRepositories
{
    /// <summary>
    /// Provides an abstraction for product repository operations.
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// Adds a new product to the repository.
        /// </summary>
        /// <param name="product">The product to create</param>
        ///  <returns>The created product's Id.</returns>
        Task<int> AddProductAsync(CreateProductDto product);

        /// <summary>
        /// Retrieves a product by it's ID.
        /// </summary>
        /// <param name="id">The ID of the product.</param>
        /// <returns>The product with the specified ID or null.</returns>
        Task<Product?> GetProductByIdAsync(int id);

        /// <summary>
        /// Updates an existing product in the repository.
        /// </summary>
        /// <param name="id">The ID of the product.</param>
        /// <param name="product">The product with updated data.</param>
        /// <returns>The boolean value means whether the product was updated</returns>
        Task<bool> UpdateProductAsync(int id, Product product);

        /// <summary>
        /// Checks whether the product exists
        /// </summary>
        /// <param name="id">The ID of the product.</param>
        /// <returns>The boolean value means whether the product exists or not</returns>
        Task<bool> ExistsAsync(int id);

        /// <summary>
        /// Deletes a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        /// <returns>The boolean value means whether the product deleted or not</returns>
        Task<bool> DeleteProductAsync(int id);

        /// <summary>
        /// Retrieves all products.
        /// </summary>
        /// <param name="options">Filtering and sorting options.</param>
        /// <returns>A list of products.</returns>
        Task<IEnumerable<Product>> GetAllProductsAsync(GetListOfProductsDto options);
    }
}
