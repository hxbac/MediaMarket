namespace MediaMarket.Application.DTO.Response.Auth
{
    public class RegisterResponse
    {
        public string Token { get; set; } = string.Empty;
        public UserResponse? User { get; set; }
    }
}
