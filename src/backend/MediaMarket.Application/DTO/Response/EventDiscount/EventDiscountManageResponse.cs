using MediaMarket.Domain.Enums;

namespace MediaMarket.Application.DTO.Response.EventDiscount
{
    public class EventDiscountManageResponse
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public DiscountType Type { get; set; }
        public float Value { get; set; }
        public long DiscountMinValue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
