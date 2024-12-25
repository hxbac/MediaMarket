using MediaMarket.Application.Bases;
using MediaMarket.Application.DTO.Request.EventDiscount;
using MediaMarket.Application.DTO.Response.EventDiscount;

namespace MediaMarket.Application.Contracts.Services
{
    public interface IEventDiscountService
    {
        Task<BaseResponse<PaginatedResult<EventDiscountManageResponse>>> GetListPaginated(GetListPaginatedRequest request);
        Task<BaseResponse<EventDiscountManageResponse>> Create(CreateDiscountRequest request);
    }
}
