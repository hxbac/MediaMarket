using MediaMarket.Domain.Enums;

namespace MediaMarket.Application.DTO.Request.Withdrawal
{
    public class ApprovalRequest
    {
        public WithdrawalStatus Status { get; set; }
        public string Note { get; set; }
    }
}
