
using Microsoft.AspNetCore.Identity;

namespace MediaMarket.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string? Name { get; set; }
        public string? Avatar { get; set; }
        public string? Address { get; set; }
        public string? Description { get; set; }
        public string? BirthDay { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}
