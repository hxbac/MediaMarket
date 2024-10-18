namespace MediaMarket.Application.DTO.Request.Auth
{
    public class RegisterRequest
    {
        public required string Name { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
    }
}
