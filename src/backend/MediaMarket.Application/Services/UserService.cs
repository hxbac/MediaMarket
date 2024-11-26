using AutoMapper;
using MediaMarket.Application.Bases;
using MediaMarket.Application.Contracts.Services;
using MediaMarket.Application.DTO.Response.Auth;
using MediaMarket.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace MediaMarket.Application.Services
{
    public class UserService(
        UserManager<User> userManager,
        IMapper mapper
    ) : BaseResponseHandler, IUserService
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<UserResponse>> GetUserInfo(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            return Success(_mapper.Map<UserResponse>(user));
        }
    }
}
