using MediaMarket.Application.Contracts.Services;
using MediaMarket.Application.DTO.Request.EventDiscount;
using MediaMarket.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace MediaMarket.API.Controllers.Admin
{
    [ApiController]
    public class EventDiscountController(IEventDiscountService eventDiscountService) : ApiBaseController
    {
        private readonly IEventDiscountService _eventDiscountService = eventDiscountService;

        [HttpGet(Router.Admin.EventDiscountRouting.Action.Index)]
        public async Task<IActionResult> Index([FromQuery] GetListPaginatedRequest request)
        {
            var response = await _eventDiscountService.GetListPaginated(request);
            return CustomResult(response);
        }

        [HttpPost(Router.Admin.EventDiscountRouting.Action.Create)]
        public async Task<IActionResult> Create([FromBody] CreateDiscountRequest request)
        {
            var response = await _eventDiscountService.Create(request);
            return CustomResult(response);
        }
    }
}
