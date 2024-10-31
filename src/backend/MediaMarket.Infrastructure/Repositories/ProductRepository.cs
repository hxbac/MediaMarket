using MediaMarket.Application.Contracts.Repositories;
using MediaMarket.Application.DTO.Response.Product;
using MediaMarket.Domain.Entities;
using MediaMarket.Infrastructure.Data;
using MediaMarket.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace MediaMarket.Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<ProductDetailResponse> GetProductActiveWithRelationship(Guid productId)
        {
            var product = await _model.Include(x => x.Categories)
                .Include(x => x.Tags)
                .Include(x => x.Preview)
                .Where(x => x.Id == productId)
                .Select(x => new ProductDetailResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    Thumbnail = x.Thumbnail,
                    ShortDescription = x.ShortDescription,
                    Description = x.Description,
                    Price = x.Price,
                    ProductType = x.ProductType,
                    ProductStatus = x.ProductStatus,
                    ProductContentStatus = x.ProductContentStatus,
                    Preview = x.Preview,
                    Tags = x.Tags,
                    Categories = x.Categories
                })
                .FirstOrFailAsync();

            return product;
        }
    }
}
