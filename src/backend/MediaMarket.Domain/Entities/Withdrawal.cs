using MediaMarket.Domain.Common;
using MediaMarket.Domain.Enums;

namespace MediaMarket.Domain.Entities
{
    public class Withdrawal : BaseAuditableEntity<Guid>
    {
        public long Amount { get; set; }
        public WithdrawalStatus WithdrawalStatus { get; set; }
        public string? Note { get; set; }
        public string? AccountId { get; set; }
        public DateTimeOffset? ProcessedAt { get; set; }

        public User? UserRequest { get; set; }
    }
}
