using AutoMapper;
using MediaMarket.Application.Bases;
using MediaMarket.Application.Contracts.Repositories;
using MediaMarket.Application.DTO.Product;
using MediaMarket.Application.DTO.Response.Product;
using MediaMarket.Application.DTO.User;
using MediaMarket.Application.Extensions;
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

        public Task<PaginatedResult<ProductUserResponse>> GetListProductsForUser(Guid userId, ProductType productType, string? name, int page, int pageSize)
        {
            var query = _model
                .Include(p => p.Tags)
                .Include(p => p.Categories)
                .Where(p => p.CreatedBy == userId);

            if (productType != ProductType.None)
            {
                query = query.Where(p => p.ProductType == productType);
            }

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.Name != null && p.Name.Contains(name));
            }

            return query
                .OrderByDescending(p => p.CreatedOn)
                .Select(p => new ProductUserResponse
                {
                    Id = p.Id,
                    Name = p.Name,
                    Slug = p.Slug,
                    Thumbnail = p.Thumbnail,
                    Price = p.Price,
                    ProductContentStatus = p.ProductContentStatus,
                    Categories = p.Categories,
                    ProductType = p.ProductType,
                    ProductStatus = p.ProductStatus,
                })
                .ToPaginatedListAsync(page, pageSize);
        }

        public Task<ProductDetailResponse> GetProductActiveWithRelationship(string slug)
        {
            return _model.Include(x => x.Categories)
                .Include(x => x.Tags)
                .Include(x => x.Preview)
                .Include(x => x.Seller)
                .IncludeActiveDiscounts()
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
                    Categories = x.Categories,
                    Seller = _mapper.Map<Seller>(x.Seller),
                    Discounts = x.ProductDiscounts
                })
                .FirstOrFailAsync();
        }

        public Task<ProductLatestVersionDTO> GetProductWithLatestVersion(string slug)
        {
            return _model
                .IncludeActiveDiscounts()
                .Where(p => p.Slug == slug)
                .Select(p => new ProductLatestVersionDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Version = 1,
                    Discounts = p.ProductDiscounts
                }).
                FirstOrFailAsync();
        }

        public Task<List<Product>> GetProductsLatest(Guid userId)
        {
            return _model.Where(p => p.CreatedBy == userId)
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
        }

        public Task<List<Guid>> GetProductIdsMatchKeyword(string keyword, int take)
        {
            return _model
                .Where(p => p.ProductStatus == ProductStatus.Active)
                .Where(p => p.ProductContentStatus == ProductContentStatus.Approved)
                .Where(p => p.Name.Contains(keyword) || p.Description.Contains(keyword))
                .OrderByDescending(p => p.Name.Contains(keyword) && p.Description.Contains(keyword))
                .ThenByDescending(p => p.Name.Contains(keyword))
                .ThenByDescending(p => p.Description.Contains(keyword))
                .Take(take)
                .Select(p => p.Id)
                .ToListAsync();
        }

        public Task<List<Guid>> GetProductIdsMatchTagName(string tagName, ICollection<Guid> excludedIds, int take)
        {
            var tagSlug = tagName.ToSlug();
            return _model
                .Where(p => p.ProductStatus == ProductStatus.Active)
                .Where(p => p.ProductContentStatus == ProductContentStatus.Approved)
                .Where(p => !excludedIds.Contains(p.Id))
                .Where(p => p.Tags.Any(t => t.Slug.Contains(tagSlug)))
                .Take(take)
                .Select(p => p.Id)
                .ToListAsync();
        }

        public Task<List<Guid>> GetProductIdsMatchCategoryName(string search, ICollection<Guid> excludedIds, int take)
        {
            return _model
                .Where(p => p.ProductStatus == ProductStatus.Active)
                .Where(p => p.ProductContentStatus == ProductContentStatus.Approved)
                .Where(p => !excludedIds.Contains(p.Id))
                .Where(p => p.Categories.Any(t => t.Name.Contains(search)))
                .Take(take)
                .Select(p => p.Id)
                .ToListAsync();
        }

        public Task<List<SearchProductResponse>> GetListProductsByIds(IEnumerable<Guid> ids)
        {
            return _model
                .Where(p => ids.Contains(p.Id))
                .Select(p => new SearchProductResponse()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Slug = p.Slug,
                    Price = p.Price,
                    ShortDescription = p.ShortDescription,
                    Thumbnail = p.Thumbnail,
                })
                .ToListAsync();
        }
    }
}
