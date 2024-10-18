using MediaMarket.Domain.Entities;

namespace MediaMarket.Application.DTO.Response.Auth
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public User user { get; set; }
    }
}
