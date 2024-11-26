using AutoMapper;
using MediaMarket.Application.Bases;
using MediaMarket.Application.Contracts.Repositories;
using MediaMarket.Application.DTO.Response.Order;
using MediaMarket.Domain.Entities;
using MediaMarket.Domain.Enums;
using MediaMarket.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MediaMarket.Infrastructure.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        private readonly IMapper _mapper;

        public OrderRepository(ApplicationDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<PaginatedResult<ProductOrderResponse>> GetListOrdersForUser(Guid userId, ProductType productType, string? name, int page, int pageSize)
        {
            var query = _model
                .Include(o => o.Buyer)
                .Include(o => o.Product)
                    .ThenInclude(product => product.Categories)
                .Include(o => o.Product)
                    .ThenInclude(product => product.Tags)
                .Where(o => o.Product.CreatedBy == userId);

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(o => o.ProductName.Contains(name));
            }

            return await query
                .Select(o => new ProductOrderResponse()
                {
                    Id = o.Product.Id,
                    Name = o.Product.Name,
                    Slug = o.Product.Slug,
                    Thumbnail = o.Product.Thumbnail,
                    Price = o.Price,
                    Categories = o.Product.Categories,
                    Tags = o.Product.Tags,
                    CreatedAt = o.CreatedOn,
                    UserBuyerId = o.Buyer.Id,
                    UserBuyerName = o.Buyer.Name,
                })
                .ToPaginatedListAsync(page, pageSize);
        }

        public async Task<PaginatedResult<ProductPurchaseResponse>> GetListPurchasesForUser(Guid userId, ProductType productType, string? name, int page, int pageSize)
        {
            var query = _model
                .Include(o => o.Product)
                    .ThenInclude(product => product.Categories)
                .Include(o => o.Product)
                    .ThenInclude(product => product.Tags)
                .Where(o => o.CreatedBy == userId);

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(o => o.ProductName.Contains(name));
            }

            return await query
                .Select(o => new ProductPurchaseResponse()
                {
                    Id = o.Product.Id,
                    Name = o.Product.Name,
                    Slug = o.Product.Slug,
                    Thumbnail = o.Product.Thumbnail,
                    Price = o.Price,
                    Categories = o.Product.Categories,
                    Tags = o.Product.Tags,
                    CreatedAt = o.CreatedOn,
                })
                .ToPaginatedListAsync(page, pageSize);
        }
    }
}
