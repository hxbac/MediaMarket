using MediaMarket.Application.Contracts.Services;
using MediaMarket.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace MediaMarket.API.Controllers.Client
{
    [ApiController]
    public class UserController(
        IUserService userService
    ) : ApiBaseController
    {
        private readonly IUserService _userService = userService;

        [HttpGet(Router.UserRouting.Action.Show)]
        public async Task<IActionResult> GetUserInfo(Guid id)
        {
            var response = await _userService.GetUserInfo(id);
            return CustomResult(response);
        }
    }
}
