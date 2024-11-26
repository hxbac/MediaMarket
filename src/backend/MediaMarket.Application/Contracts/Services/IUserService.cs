using MediaMarket.Application.Bases;
using MediaMarket.Application.DTO.Response.Auth;

namespace MediaMarket.Application.Contracts.Services
{
    public interface IUserService
    {
        Task<BaseResponse<UserResponse>> GetUserInfo(Guid id);
    }
}
