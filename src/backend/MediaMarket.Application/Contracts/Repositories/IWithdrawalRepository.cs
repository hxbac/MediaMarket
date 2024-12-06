using MediaMarket.Application.Bases;
using MediaMarket.Application.DTO.Request.Withdrawal;
using MediaMarket.Application.DTO.Response.Withdrawal;
using MediaMarket.Domain.Entities;

namespace MediaMarket.Application.Contracts.Repositories
{
    public interface IWithdrawalRepository : IBaseRepository<Withdrawal>
    {
        Task<PaginatedResult<WithdrawalManageResponse>> GetListPaginated(GetListWithdrawalPaginatedRequest request);
        Task<PaginatedResult<WithdrawlCurrentUserResponse>> GetListPaginatedForUser(GetListWithdrawalCurrentUserRequest request, Guid userId);
    }
}
