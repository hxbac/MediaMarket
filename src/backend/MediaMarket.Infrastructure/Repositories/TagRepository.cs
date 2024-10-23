using MediaMarket.Application.Contracts.Repositories;
using MediaMarket.Domain.Entities;
using MediaMarket.Infrastructure.Data;

namespace MediaMarket.Infrastructure.Repositories
{
    public class TagRepository : BaseRepository<Tag>, ITagRepository
    {
        public TagRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
