
using Microsoft.AspNetCore.Identity;

namespace MediaMarket.Domain.Entities
{
    public class User : IdentityUser
    {
        public string? Name { get; set; }
        public string? Avatar { get; set; }
        public string? Address { get; set; }
        public string? Description { get; set; }
    }
}
