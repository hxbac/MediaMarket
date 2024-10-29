using MediaMarket.Application.Contracts.Repositories;
using MediaMarket.Domain.Entities;
using MediaMarket.Infrastructure.Data;

namespace MediaMarket.Infrastructure.Repositories
{
    public class PreviewRepository : BaseRepository<Preview>, IPreviewRepository
    {
        public PreviewRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
