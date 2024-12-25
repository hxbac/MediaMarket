using MediaMarket.Domain.Enums;

namespace MediaMarket.Application.DTO.Request.EventDiscount
{
    public class CreateDiscountRequest
    {
        public required string Name { get; set; }
        public DiscountType Type { get; set; }
        public float Value { get; set; }
        public long DiscountMinValue { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
