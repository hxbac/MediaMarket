using MediaMarket.Application.Contracts.Repositories;
using MediaMarket.Domain.Entities;
using MediaMarket.Infrastructure.Data;

namespace MediaMarket.Infrastructure.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryId(Guid categoryId)
        {
            var categoryWithProducts = await _model.Where(c => c.Id == categoryId)
                .Select(c => new
                {
                    Products = c.Products.Take(8).ToList(),
                }).FirstOrFailAsync();

            return categoryWithProducts.Products;
        }
    }
}
