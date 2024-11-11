using MediaMarket.Application.Contracts.Repositories;
using MediaMarket.Domain.Entities;
using MediaMarket.Infrastructure.Data;

namespace MediaMarket.Infrastructure.Repositories
{
    public class VideoSolutionRepository : BaseRepository<VideoSolution>, IVideoSolutionRepository
    {
        public VideoSolutionRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
