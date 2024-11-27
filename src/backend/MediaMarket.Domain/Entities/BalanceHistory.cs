using MediaMarket.Domain.Common;
using MediaMarket.Domain.Enums;

namespace MediaMarket.Domain.Entities
{
    public class BalanceHistory : BaseEntity<Guid>
    {
        public Guid UserId { get; set; }
        public long PreviousBalance { get; set; }
        public long NewBalance { get; set; }
        public Guid TransactionId { get; set; }
        public TransactionType TransactionType { get; set; }
        public DateTimeOffset CreatedOn { get; set; } = DateTimeOffset.Now;

        public User? User { get; set; }
    }
}
