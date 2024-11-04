namespace MediaMarket.Application.DTO.Response.Auth
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public UserResponse? User { get; set; }
    }
}
