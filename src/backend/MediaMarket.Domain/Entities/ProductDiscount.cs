using MediaMarket.Domain.Common;
using MediaMarket.Domain.Enums;

namespace MediaMarket.Domain.Entities
{
    public class ProductDiscount : BaseEntity<Guid>
    {
        public Guid ProductId { get; set; }
        public Product? Product { get; }
        public DiscountType Type { get; set; }
        public float Value { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
