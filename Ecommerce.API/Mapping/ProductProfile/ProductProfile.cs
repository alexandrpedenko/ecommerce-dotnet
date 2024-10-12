using AutoMapper;
using Ecommerce.API.Contracts.Dtos.Products;
using Ecommerce.API.Contracts.Requests.Products;
using Ecommerce.API.Contracts.Responses.Products;
using Ecommerce.API.Domain;

namespace Ecommerce.API.Mapping.ProductProfile
{
    public class ProductProfile : Profile
    {

        public ProductProfile()
        {
            CreateMap<GetListOfProductsRequestDto, GetListOfProductsDto>();
            CreateMap<Product, GetProductResponseDto>();
        }
    }
}
