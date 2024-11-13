using MediaMarket.Application.Bases;
using MediaMarket.Application.DTO.Request.Order;
using MediaMarket.Application.DTO.Response.Order;

namespace MediaMarket.Application.Contracts.Services
{
    public interface IOrderService
    {
        Task<BaseResponse<CreateOrderResponse>> CreateOrder(CreateOrderRequest request, Guid userId);
        Task<BaseResponse<CallbackStripeResponse>> CallbackStripe(CallbackStripeRequest request);
    }
}
