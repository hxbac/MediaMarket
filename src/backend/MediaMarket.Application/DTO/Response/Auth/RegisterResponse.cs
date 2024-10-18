using MediaMarket.Domain.Entities;

namespace MediaMarket.Application.DTO.Response.Auth
{
    public class RegisterResponse
    {
        public string Token { get; set; } = string.Empty;
        public User User { get; set; }
    }
}
