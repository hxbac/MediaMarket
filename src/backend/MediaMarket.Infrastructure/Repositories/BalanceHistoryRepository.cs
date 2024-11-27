using MediaMarket.Application.Contracts.Repositories;
using MediaMarket.Domain.Entities;
using MediaMarket.Infrastructure.Data;

namespace MediaMarket.Infrastructure.Repositories
{
    public class BalanceHistoryRepository : BaseRepository<BalanceHistory>, IBalanceHistoryRepository
    {
        public BalanceHistoryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
