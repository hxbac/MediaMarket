using MediaMarket.Application.Contracts.Services;
using MediaMarket.Application.DTO.Request.Order;
using MediaMarket.Application.DTO.Request.Product;
using MediaMarket.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediaMarket.API.Controllers.Client
{
    [ApiController]
    public class OrderController(IOrderService orderService) : ApiBaseController
    {
        private readonly IOrderService _orderService = orderService;

        [Authorize]
        [HttpPost(Router.OrderRouting.Action.Create)]
        public async Task<IActionResult> Create([FromBody] CreateOrderRequest request)
        {
            var response = await _orderService.CreateOrder(request);
            return CustomResult(response);
        }

        [Authorize]
        [HttpPost(Router.OrderRouting.Action.StripeCallback)]
        public async Task<IActionResult> StripeCallback(CallbackStripeRequest request)
        {
            var response = await _orderService.CallbackStripe(request);
            return CustomResult(response);
        }

        [Authorize]
        [HttpGet(Router.OrderRouting.Action.MyPurchases)]
        public async Task<IActionResult> GetMyPurchases([FromQuery] GetProductListRequest request)
        {
            var response = await _orderService.GetMyPurchasesPaginated(request);
            return CustomResult(response);
        }

        [Authorize]
        [HttpGet(Router.OrderRouting.Action.MyOrders)]
        public async Task<IActionResult> GetMyOrders([FromQuery] GetProductListRequest request)
        {
            var response = await _orderService.GetMyOrdersPaginated(request);
            return CustomResult(response);
        }
    }
}
