using MediaMarket.Application.Bases;
using MediaMarket.Application.Contracts.Repositories;
using MediaMarket.Application.DTO.Request.Withdrawal;
using MediaMarket.Application.DTO.Response.Withdrawal;
using MediaMarket.Domain.Entities;
using MediaMarket.Infrastructure.Data;

namespace MediaMarket.Infrastructure.Repositories
{
    public class WithdrawalRepository : BaseRepository<Withdrawal>, IWithdrawalRepository
    {
        public WithdrawalRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Task<PaginatedResult<WithdrawlCurrentUserResponse>> GetListPaginatedForUser(GetListWithdrawalCurrentUserRequest request, Guid userId)
        {
            return _model.Where(x => x.CreatedBy == userId)
                .Select(x => new WithdrawlCurrentUserResponse()
                {
                    Id = x.Id,
                    Amount = x.Amount,
                    Note = x.Note,
                    ProcessedAt = x.ProcessedAt,
                    WithdrawalStatus = x.WithdrawalStatus
                }).ToPaginatedListAsync(request.Page, request.PageSize);
        }
    }
}
