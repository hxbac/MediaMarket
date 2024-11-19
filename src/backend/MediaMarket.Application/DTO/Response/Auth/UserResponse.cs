namespace MediaMarket.Application.DTO.Response.Auth
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? UserName { get; set; }
        public string? Avatar { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? Description { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Birthday { get; set; }
    }
}
