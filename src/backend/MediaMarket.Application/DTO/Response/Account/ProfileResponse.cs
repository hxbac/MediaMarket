namespace MediaMarket.Application.DTO.Response.Account
{
    public class ProfileResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Avatar { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? BirthDay { get; set; }
    }
}
