using MediaMarket.Application.Contracts.Repositories;
using MediaMarket.Domain.Entities;
using MediaMarket.Infrastructure.Data;

namespace MediaMarket.Infrastructure.Repositories
{
    public class ImageRepository : BaseRepository<Image>, IImageRepository
    {
        public ImageRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
