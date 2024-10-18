namespace MediaMarket.Application.DTO.Request.Auth
{
    public class LoginRequest
    {
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public required string Password { get; set; }
    }
}
