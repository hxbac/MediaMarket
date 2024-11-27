using MediaMarket.Application.Contracts.Repositories;
using MediaMarket.Domain.Entities;
using MediaMarket.Infrastructure.Data;

namespace MediaMarket.Infrastructure.Repositories
{
    public class WithdrawalRepository : BaseRepository<Withdrawal>, IWithdrawalRepository
    {
        public WithdrawalRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
