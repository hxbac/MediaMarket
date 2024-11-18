using AutoMapper;
using MediaMarket.Application.DTO.Response.Order;
using MediaMarket.Application.DTO.Response.Product;
using MediaMarket.Domain.Entities;

namespace MediaMarket.Application.Mappings
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductHomePageResponse>();
            CreateMap<Product, CreateProductResponse>();
            CreateMap<Product, ProductUserResponse>();
            CreateMap<Product, ProductPurchaseResponse>();
            CreateMap<Product, ProductOrderResponse>();
            CreateMap<Product, ProductLatestResponse>();
        }
    }
}
