using MediaMarket.Application.Contracts.Services;
using MediaMarket.Application.DTO.Request.Auth;
using MediaMarket.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediaMarket.API.Controllers.Client
{
    [ApiController]
    public class AuthController(IAuthService authService) : ApiBaseController
    {
        private readonly IAuthService _authService = authService;

        [HttpPost(Router.AuthRouting.Action.Register)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var response = await _authService.Register(request);
            return CustomResult(response);
        }

        [HttpPost(Router.AuthRouting.Action.Login)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var response = await _authService.Login(request);
            return CustomResult(response);
        }

        [Authorize]
        [HttpGet(Router.AuthRouting.Action.GetProfile)]
        public async Task<IActionResult> GetProfile()
        {
            var response = await _authService.GetProfile();
            return CustomResult(response);
        }
    }
}
