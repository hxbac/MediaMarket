using MediaMarket.Application.Contracts.Repositories;
using MediaMarket.Domain.Entities;
using MediaMarket.Infrastructure.Data;

namespace MediaMarket.Infrastructure.Repositories
{
    public class ProductDetailRepository : BaseRepository<ProductDetail>, IProductDetailRepository
    {
        public ProductDetailRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Task<ProductDetail> GetLatestVersion(Guid ProductId)
        {
            throw new NotImplementedException();
        }
    }
}
