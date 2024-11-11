using MediaMarket.Domain.Common;

namespace MediaMarket.Domain.Entities
{
    public class Tag : BaseEntity<Guid>
    {
        public string? Name { get; set; }
        public string? Slug { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
