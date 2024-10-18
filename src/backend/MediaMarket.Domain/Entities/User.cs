
using Microsoft.AspNetCore.Identity;

namespace MediaMarket.Domain.Entities
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
    }
}
