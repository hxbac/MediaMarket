﻿using MediaMarket.Application.Contracts.Services;
using MediaMarket.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediaMarket.API.Controllers.Client
{
    [ApiController]
    public class WithdrawalController(IWithdrawalService withdrawalService) : ApiBaseController
    {
        private readonly IWithdrawalService _withdrawalService = withdrawalService;

        [Authorize]
        [HttpPost(Router.OrderRouting.Action.Create)]
        public async Task<IActionResult> CreateRequest([FromBody] CreateWithdrawalRequest request)
        {
            var response = await _withdrawalService.CreateRequest(request);
            return CustomResult(response);
        }
    }
}
