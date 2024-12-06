using MediaMarket.Application.Bases;
using MediaMarket.Application.DTO.Request.User;
using MediaMarket.Application.DTO.Response.User;
using MediaMarket.Domain.Entities;

namespace MediaMarket.Application.Contracts.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<PaginatedResult<UserManageResponse>> GetListPaginated(GetListUserPaginatedRequest request);
    }
}
