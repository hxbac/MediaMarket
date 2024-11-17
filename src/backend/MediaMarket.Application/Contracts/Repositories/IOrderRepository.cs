using MediaMarket.Application.Bases;
using MediaMarket.Application.DTO.Response.Order;
using MediaMarket.Domain.Entities;
using MediaMarket.Domain.Enums;

namespace MediaMarket.Application.Contracts.Repositories
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task<PaginatedResult<ProductPurchaseResponse>> GetListPurchasesForUser(Guid userId, ProductType productType, string? name, int page, int pageSize);
        Task<PaginatedResult<ProductOrderResponse>> GetListOrdersForUser(Guid userId, ProductType productType, string? name, int page, int pageSize);
    }
}
