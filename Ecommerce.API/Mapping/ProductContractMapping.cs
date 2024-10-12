using Ecommerce.API.Contracts.Responses.Products;
using Ecommerce.API.Domain;

namespace Ecommerce.API.Mapping
{
    // NOTE: Just for showing the manual mapping possibility
    public static class ProductContractMapping
    {
        public static GetProductResponseDto MapToGetByIdResponse(this Product product)
        {
            return new GetProductResponseDto
            {
                Id = product.Id,
                Title = product.Title,
                Description = product.Description,
                Price = product.Price
            };
        }

    }
}
