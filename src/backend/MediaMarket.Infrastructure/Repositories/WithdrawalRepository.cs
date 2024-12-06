using MediaMarket.Application.Bases;
using MediaMarket.Application.Contracts.Repositories;
using MediaMarket.Application.DTO.Request.Withdrawal;
using MediaMarket.Application.DTO.Response.Withdrawal;
using MediaMarket.Domain.Entities;
using MediaMarket.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MediaMarket.Infrastructure.Repositories
{
    public class WithdrawalRepository : BaseRepository<Withdrawal>, IWithdrawalRepository
    {
        public WithdrawalRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Task<PaginatedResult<WithdrawalManageResponse>> GetListPaginated(GetListWithdrawalPaginatedRequest request)
        {
            return _model
                .Include(x => x.UserRequest)
                .Select(x => new WithdrawalManageResponse()
                {
                    Id = x.Id,
                    Amount = x.Amount,
                    Note = x.Note,
                    ProcessedAt = x.ProcessedAt,
                    WithdrawalStatus = x.WithdrawalStatus,
                    CreatedOn = x.CreatedOn,
                    UserCreated = x.UserRequest.Id,
                    NameOfUser = x.UserRequest.Name
                })
                .ToPaginatedListAsync(request.Page, request.PageSize);
        }

        public Task<PaginatedResult<WithdrawlCurrentUserResponse>> GetListPaginatedForUser(GetListWithdrawalCurrentUserRequest request, Guid userId)
        {
            return _model.Where(x => x.CreatedBy == userId)
                .OrderByDescending(x => x.CreatedOn)
                .Select(x => new WithdrawlCurrentUserResponse()
                {
                    Id = x.Id,
                    Amount = x.Amount,
                    Note = x.Note,
                    ProcessedAt = x.ProcessedAt,
                    WithdrawalStatus = x.WithdrawalStatus,
                    CreatedOn = x.CreatedOn,
                }).ToPaginatedListAsync(request.Page, request.PageSize);
        }
    }
}
