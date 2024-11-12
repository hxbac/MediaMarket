using MediaMarket.Application.Contracts.Services;
using MediaMarket.Application.DTO.Request.Order;
using MediaMarket.Domain.Common;
using MediaMarket.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System.Security.Claims;

namespace MediaMarket.API.Controllers.Client
{
    [ApiController]
    public class OrderController(IOrderService _orderService) : ApiBaseController
    {
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

        [HttpPost(Router.OrderRouting.Action.StripeCallback)]
        public async Task<IActionResult> StripeFulfillAsync()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            try
            {
                var stripeEvent = EventUtility.ConstructEvent(
                  json,
                  Request.Headers["Stripe-Signature"],
                  "whsec_f30f487e64d08ab98d19bb05b219855752980eb4168fc08b10611fc61fae5562"
                );

                // If on SDK version < 46, use class Events instead of EventTypes
                if (
                  stripeEvent.Type == EventTypes.CheckoutSessionCompleted ||
                  stripeEvent.Type == EventTypes.CheckoutSessionAsyncPaymentSucceeded
                )
                {
                    var session = stripeEvent.Data.Object as Session;

                    FulfillCheckout(session.Id);
                }

                return Ok();
            }
            catch (StripeException)
            {
                return BadRequest();
            }
        }
    }
}
