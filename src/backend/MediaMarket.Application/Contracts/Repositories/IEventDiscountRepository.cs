using MediaMarket.Application.Bases;
using MediaMarket.Application.DTO.Request.EventDiscount;
using MediaMarket.Application.DTO.Response.EventDiscount;
using MediaMarket.Domain.Entities;

namespace MediaMarket.Application.Contracts.Repositories
{
    public interface IEventDiscountRepository : IBaseRepository<EventDiscount>
    {
        Task<PaginatedResult<EventDiscountManageResponse>> GetListPaginated(GetListPaginatedRequest request);
        Task<List<EventDiscount>> GetEventDiscountActive();
    }
}
