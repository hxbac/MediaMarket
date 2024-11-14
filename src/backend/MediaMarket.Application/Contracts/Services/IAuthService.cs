using MediaMarket.Application.Bases;
using MediaMarket.Application.DTO.Request.Auth;
using MediaMarket.Application.DTO.Response.Auth;

namespace MediaMarket.Application.Contracts.Services
{
    public interface IAuthService
    {
        Task<BaseResponse<RegisterResponse>> Register(RegisterRequest request);
        Task<BaseResponse<LoginResponse>> Login(LoginRequest request);
        Task<BaseResponse<UserResponse>> GetProfile();
    }
}
