using MediaMarket.Application.Contracts.Repositories;
using MediaMarket.Domain.Entities;
using MediaMarket.Infrastructure.Data;

namespace MediaMarket.Infrastructure.Repositories
{
    public class ProductDiscountRepository : BaseRepository<ProductDiscount>, IProductDiscountRepository
    {
        public ProductDiscountRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
