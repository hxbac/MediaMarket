using AutoMapper;
using MediaMarket.Application.DTO.Response.Withdrawal;
using MediaMarket.Domain.Entities;

namespace MediaMarket.Application.Mappings
{
    public class WithdrawalMappingProfile : Profile
    {
        public WithdrawalMappingProfile()
        {
            CreateMap<Withdrawal, CreateWithdrawalResponse>();
            CreateMap<Withdrawal, WithdrawlCurrentUserResponse>();
        }
    }
}
