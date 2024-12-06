using MediaMarket.Domain.Enums;

namespace MediaMarket.Application.DTO.Response.Withdrawal
{
    public class WithdrawalManageResponse
    {
        public Guid Id { get; set; }
        public long Amount { get; set; }
        public WithdrawalStatus WithdrawalStatus { get; set; }
        public string? Note { get; set; }
        public DateTimeOffset? ProcessedAt { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public Guid UserCreated { get; set; }
        public string NameOfUser { get; set; }
    }
}
