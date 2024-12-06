using MediaMarket.Application.Contracts.Services;
using MediaMarket.Application.DTO.Request.Withdrawal;
using MediaMarket.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace MediaMarket.API.Controllers.Admin
{
    [ApiController]
    public class WithdrawalController(IWithdrawalService withdrawalService) : ApiBaseController
    {
        private readonly IWithdrawalService _withdrawalService = withdrawalService;

        [HttpGet(Router.Admin.WithdrawalRouting.Action.Index)]
        public async Task<IActionResult> Index([FromQuery] GetListWithdrawalPaginatedRequest request)
        {
            var response = await _withdrawalService.GetListPaginated(request);
            return CustomResult(response);
        }
    }
}
