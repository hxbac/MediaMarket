using AutoMapper;
using MediaMarket.Application.DTO.Request.Tag;
using MediaMarket.Application.DTO.Response.Tag;
using MediaMarket.Domain.Entities;

namespace MediaMarket.Application.Mappings
{
    public class TagMappingProfile : Profile
    {
        public TagMappingProfile()
        {
            CreateMap<Tag, TagResponse>();
            CreateMap<CreateTagRequest, Tag>();
            CreateMap<UpdateTagRequest, Tag>();
        }
    }
}
