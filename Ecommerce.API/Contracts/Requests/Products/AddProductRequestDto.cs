namespace Ecommerce.API.Contracts.Requests.Products
{
    /// <summary>
    /// Create product dto
    /// </summary>
    public class AddProductRequestDto
    {
        public required string Title { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }
    }
}
