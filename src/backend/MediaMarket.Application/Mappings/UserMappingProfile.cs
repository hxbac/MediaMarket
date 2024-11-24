using AutoMapper;
using MediaMarket.Application.DTO.Response.Auth;
using MediaMarket.Application.DTO.User;
using MediaMarket.Domain.Entities;

namespace MediaMarket.Application.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserResponse>();
            CreateMap<User, Seller>();
        }
    }
}
