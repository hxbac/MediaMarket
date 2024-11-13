namespace MediaMarket.Application.DTO.Payment
{
    public class CreatePaymentDTO
    {
        public string? RedirectUrl { get; set; }
        public string? PaymentSession { get; set; }
    }
}
