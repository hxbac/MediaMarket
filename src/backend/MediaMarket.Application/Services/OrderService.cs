using MediaMarket.Application.Bases;
using MediaMarket.Application.Contracts.Services;
using MediaMarket.Application.DTO.Request.Order;
using MediaMarket.Application.DTO.Response.Order;

namespace MediaMarket.Application.Services
{
    public class OrderService(IPaymentService _paymentService) : BaseResponseHandler, IOrderService
    {

        public async Task<BaseResponse<CreateOrderResponse>> CreateOrder(CreateOrderRequest request, Guid userId)
        {
            string url = _paymentService.Create();

            return Success(new CreateOrderResponse()
            {
                RedirectUrl = url,
            });
        }
    }
}
