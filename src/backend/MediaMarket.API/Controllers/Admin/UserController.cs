using MediaMarket.Application.Contracts.Services;
using MediaMarket.Application.DTO.Request.User;
using MediaMarket.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace MediaMarket.API.Controllers.Admin
{
    [ApiController]
    public class UserController(IUserService userService) : ApiBaseController
    {
        private readonly IUserService _userService = userService;

        [HttpGet(Router.Admin.UserRouting.Action.Index)]
        public async Task<IActionResult> Index([FromQuery] GetListUserPaginatedRequest request)
        {
            var response = await _userService.GetListPaginated(request);
            return CustomResult(response);
        }
    }
}
