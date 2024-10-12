namespace Ecommerce.API.DataEF.Models
{
    /// <summary>
    /// Product model
    /// </summary>
    public class ProductModel
    {
        /// <summary>
        ///  Product id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///  title
        /// </summary>
        public required string Title { get; set; }

        /// <summary>
        ///  Product description
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        ///  Product price
        /// </summary>
        public decimal Price { get; set; }
    }
}
