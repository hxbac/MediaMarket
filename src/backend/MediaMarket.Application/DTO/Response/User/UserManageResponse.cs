namespace MediaMarket.Application.DTO.Response.User
{
    public class UserManageResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public long Balance { get; set; }
        public bool LockoutEnabled { get; set; }
        public int NumberOrder { get; set; }
        public int NumberProduct { get; set; }
    }
}
