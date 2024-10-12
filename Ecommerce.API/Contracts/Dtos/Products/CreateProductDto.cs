namespace Ecommerce.API.Contracts.Dtos.Products
{
    public class CreateProductDto
    {
        public required string Title { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }
    }
}
