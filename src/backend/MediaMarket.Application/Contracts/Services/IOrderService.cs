using MediaMarket.Application.Bases;
using MediaMarket.Application.DTO.Request.Order;
using MediaMarket.Application.DTO.Request.Product;
using MediaMarket.Application.DTO.Response.Order;

namespace MediaMarket.Application.Contracts.Services
{
    public interface IOrderService
    {
        Task<BaseResponse<CreateOrderResponse>> CreateOrder(CreateOrderRequest request);
        Task<BaseResponse<CallbackStripeResponse>> CallbackStripe(CallbackStripeRequest request);
        Task<BaseResponse<PaginatedResult<ProductPurchaseResponse>>> GetMyPurchasesPaginated(GetProductListRequest request);
        Task<BaseResponse<PaginatedResult<ProductOrderResponse>>> GetMyOrdersPaginated(GetProductListRequest request);
        Task<BaseResponse<object>> DownloadProductOrder(Guid id);
    }
}
