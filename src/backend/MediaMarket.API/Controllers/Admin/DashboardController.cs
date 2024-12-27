using MediaMarket.Application.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace MediaMarket.API.Controllers.Admin
{
    [ApiController]
    public class DashboardController(IOrderService orderService) : ApiBaseController
    {
        private readonly IOrderService _orderService = orderService;

        //[HttpGet(Router.Admin.DashboardRouting.Action.Statistics)]
        //public async Task<IActionResult> Statistics()
        //{
        //    var response = await _orderService.GetStatisticsTotalRevenue();
        //    return Ok(response);
        //}
    }
}
