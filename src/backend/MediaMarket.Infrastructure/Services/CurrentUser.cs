using MediaMarket.Application.Contracts.Common;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace MediaMarket.Infrastructure.Services
{
    public class CurrentUser : IUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid? Id
        {
            get
            {
                var idString = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
                return Guid.TryParse(idString, out var id) ? id : null;
            }
        }
    }
}
