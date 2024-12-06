using MediaMarket.Application.Bases;
using MediaMarket.Application.DTO.Request.User;
using MediaMarket.Application.DTO.Response.Auth;
using MediaMarket.Application.DTO.Response.User;

namespace MediaMarket.Application.Contracts.Services
{
    public interface IUserService
    {
        Task<BaseResponse<PaginatedResult<UserManageResponse>>> GetListPaginated(GetListUserPaginatedRequest request);
        Task<BaseResponse<UserResponse>> GetUserInfo(Guid id);
    }
}
