using Ecommerce.API.DataEF.Models;
using Ecommerce.API.Domain;
using System.Linq.Expressions;

namespace Ecommerce.API.DataEF.Converters.ProductConverter
{
    public static class ProductConverter
    {
        public static Expression<Func<ProductModel, Product>> ToDomain()
        {
            return p => new Product
            {
                Id = p.Id,
                Title = p.Title,
                Price = p.Price,
                Description = p.Description ?? string.Empty,
            };
        }
    }
}
