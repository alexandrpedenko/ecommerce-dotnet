using Ecommerce.API.Exceptions.Common;

namespace Ecommerce.API.Exceptions.Products
{
    public class ProductNotFoundException(int id) : NotFoundException($"Product with id: {id} not found")
    {
    }
}
