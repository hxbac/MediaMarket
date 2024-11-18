using AutoMapper;
using MediaMarket.Application.Bases;
using MediaMarket.Application.Contracts.Repositories;
using MediaMarket.Application.DTO.Product;
using MediaMarket.Application.DTO.Response.Product;
using MediaMarket.Domain.Entities;
using MediaMarket.Domain.Enums;
using MediaMarket.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MediaMarket.Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private readonly IMapper _mapper;

        public ProductRepository(ApplicationDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<PaginatedResult<ProductUserResponse>> GetListProductsForUser(Guid userId, ProductType productType, string? name, int page, int pageSize)
        {
            var query = _model
                .Include(p => p.Tags)
                .Include(p => p.Categories)
                .Where(p => p.CreatedBy == userId && p.ProductType == productType);

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.Name != null && p.Name.Contains(name));
            }

            return await query
                .Select(p => new Product
                {
                    Id = p.Id,
                    Name = p.Name,
                    Slug = p.Slug,
                    Thumbnail = p.Thumbnail,
                    Price = p.Price,
                    ProductContentStatus = p.ProductContentStatus,
                    Categories = p.Categories,
                })
                .ToPaginatedListAsync<Product, ProductUserResponse>(page, pageSize, _mapper);
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

        public async Task<IEnumerable<Product>> GetProductsLatest(Guid userId)
        {
            var products = await _model.Where(p => p.CreatedBy == userId)
                .Where(p => p.ProductStatus == ProductStatus.Active)
                .OrderBy(p => p.CreatedOn)
                .Take(8)
                .Select(p => new Product
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Slug = p.Slug,
                    Thumbnail = p.Thumbnail,
                    ShortDescription = p.ShortDescription,
                    ProductType = p.ProductType,
                }).ToListAsync();

            return products;
        }
    }
}
