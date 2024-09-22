namespace Ecommerce.API.Contracts.Requests.Common
{
    public class PaginationRequestDto
    {
        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public string? SortBy { get; set; }

        public string? SortOrder { get; set; }

        public string? Search { get; set; }
    }
}
