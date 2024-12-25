using MediaMarket.Domain.Common;
using MediaMarket.Domain.Enums;

namespace MediaMarket.Domain.Entities
{
    public class Order : BaseAuditableEntity<Guid>
    {
        public Guid ProductId { get; set; }
        public string? ProductName { get; set; }
        public long Price { get; set; }
        public long AdminRevenue { get; set; }
        public long SellerRevenue { get; set; }
        public OrderStatus Status { get; set; }
        public string? PaymentMethod { get; set; }
        public Guid PaymentId { get; set; }
        public int ProductVersion { get; set; }
        public string? PaymentSession { get; set; }

        public Product? Product { get; set; }
        public User? Buyer { get; set; }
    }
}
