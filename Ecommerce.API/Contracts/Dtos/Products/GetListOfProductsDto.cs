using Ecommerce.API.Contracts.Requests.Common;

namespace Ecommerce.API.Contracts.Dtos.Products
{
    public class GetListOfProductsDto : PaginationRequestDto
    {
        public decimal? MinPrice { get; set; }

        public decimal? MaxPrice { get; set; }
    }
}
