namespace MediaMarket.Application.DTO.Response.Order
{
    public class CallbackStripeResponse
    {
        public Domain.Entities.Order? Order { get; set; }
        public bool IsSuccess { get; set; }
    }
}
