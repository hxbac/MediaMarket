using AutoMapper;
using MediaMarket.Application.DTO.Request.EventDiscount;
using MediaMarket.Application.DTO.Response.EventDiscount;
using MediaMarket.Domain.Entities;

namespace MediaMarket.Application.Mappings
{
    public class EventDiscountMappingProfile : Profile
    {
        public EventDiscountMappingProfile()
        {
            CreateMap<CreateDiscountRequest, EventDiscount>();
            CreateMap<EventDiscount, EventDiscountManageResponse>();
        }
    }
}
