namespace Ecommerce.API.Contracts.Responses.Common
{
    public class PaginatedListResponseDto<T>
    {
        public required IEnumerable<T> Results { get; init; } = [];
    }
}
