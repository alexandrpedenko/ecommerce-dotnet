using Ecommerce.API.Contracts.Requests.Common;

namespace Ecommerce.API.Contracts.Requests.Products
{
    public class GetListOfProductsRequestDto : PaginationRequestDto
    {
        public decimal? MinPrice { get; set; }

        public decimal? MaxPrice { get; set; }
    }
}
