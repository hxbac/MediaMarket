using MediaMarket.Application.Contracts.Repositories;
using MediaMarket.Application.DTO.Product;
using MediaMarket.Application.DTO.Response.Product;
using MediaMarket.Domain.Entities;
using MediaMarket.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MediaMarket.Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<ProductDetailResponse> GetProductActiveWithRelationship(string slug)
        {
            var product = await _model.Include(x => x.Categories)
                .Include(x => x.Tags)
                .Include(x => x.Preview)
                .Where(x => x.Slug == slug)
                .Select(x => new ProductDetailResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    Slug = x.Slug,
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

        public async Task<ProductLatestVersionDTO> GetProductWithLatestVersion(string slug)
        {
            var product = await _model.Where(p => p.Slug == slug)
                .Where(p => p.ProductStatus == Domain.Enums.ProductStatus.Active)
                .Select(p => new ProductLatestVersionDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Version = p.ProductDetails
                        .Max(pd => pd.Version)
                }).
                FirstOrFailAsync();
            return product;
        }
    }
}
