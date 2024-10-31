using AutoMapper;
using MediaMarket.Application.DTO.Response.Category;
using MediaMarket.Domain.Entities;

namespace MediaMarket.Application.Mappings
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<Category, CategoryResponse>();
        }
    }
}
