using MediaMarket.Domain.Common;

namespace MediaMarket.Domain.Entities
{
    public class Category : BaseAuditableEntity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Order { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
