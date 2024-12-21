using MediaMarket.Application.Bases;
using MediaMarket.Application.DTO.Request.Withdrawal;
using MediaMarket.Application.DTO.Response.Withdrawal;

namespace MediaMarket.Application.Contracts.Services
{
    public interface IWithdrawalService
    {
        Task<BaseResponse<object>> ApprovalRequest(Guid id, ApprovalRequest request);
        Task<BaseResponse<CreateWithdrawalResponse>> CreateRequest(CreateWithdrawalRequest request);
        Task<BaseResponse<PaginatedResult<WithdrawalManageResponse>>> GetListPaginated(GetListWithdrawalPaginatedRequest request);
        Task<BaseResponse<PaginatedResult<WithdrawlCurrentUserResponse>>> GetListWithdrawalForCurrentUser(GetListWithdrawalCurrentUserRequest request);
    }
}
