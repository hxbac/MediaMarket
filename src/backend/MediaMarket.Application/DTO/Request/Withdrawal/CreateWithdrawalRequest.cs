namespace MediaMarket.Application.DTO.Request.Withdrawal
{
    public class CreateWithdrawalRequest
    {
        public long Amount { get; set; }
        public string CardToken { get; set; }
    }
}
