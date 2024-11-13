using MediaMarket.Application.Contracts.Services;
using MediaMarket.Application.DTO.Request.Order;
using MediaMarket.Domain.Common;
using MediaMarket.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            var subJwt = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isGuidValid = Guid.TryParse(subJwt, out var userId);
            if (!isGuidValid)
            {
                throw new SecurityTokenException();
            }

            var response = await _orderService.CreateOrder(request, userId);
            return CustomResult(response);
        }

        [Authorize]
        [HttpPost(Router.OrderRouting.Action.StripeCallback)]
        public async Task<IActionResult> StripeCallback(CallbackStripeRequest request)
        {
            var response = await _orderService.CallbackStripe(request);
            return CustomResult(response);
        }
    }
}
