namespace Ecommerce.API.Domain
{
    /// <summary>
    /// Product domain
    /// </summary>
    public class Product
    {
        public int Id { get; set; }

        public required string Title { get; set; }

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }
    }
}
