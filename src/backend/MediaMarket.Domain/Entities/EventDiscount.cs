using MediaMarket.Domain.Common;
using MediaMarket.Domain.Enums;

namespace MediaMarket.Domain.Entities
{
    public class EventDiscount : BaseAuditableEntity<Guid>
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public DiscountType Type { get; set; }
        public float Value { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DiscountApplicableType ApplicableType { get; set; }
    }
}
