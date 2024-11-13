using MediaMarket.Application.Contracts.Services;
using MediaMarket.Application.DTO.Payment;
using MediaMarket.Application.DTO.Product;
using Stripe;
using Stripe.Checkout;

namespace MediaMarket.Infrastructure.Payment
{
    public class StripePaymentService : IPaymentService
    {
        public StripePaymentService()
        {
            StripeConfiguration.ApiKey = "sk_test_51QJpfyGOIY1d15fJqpCqCaqKVpiyd54j6YRs45TRMDBGfTgJAIwcazenx2CCQqt8WrQi9uc30WOE1yEDlZrpEh6S00Auquaj9H";
        }

        public CreatePaymentDTO Create(ProductLatestVersionDTO product)
        {
            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmount = product.Price,
                    Currency = "VND",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = product.Name,
                    },
                },
                    Quantity = 1,
                },
            },
                Mode = "payment",
                SuccessUrl = "http://localhost:8080/payment/result?session_id={CHECKOUT_SESSION_ID}",
                CancelUrl = "http://localhost:8080",
            };

            var service = new SessionService();
            Session session = service.Create(options);

            return new CreatePaymentDTO()
            {
                RedirectUrl = session.Url,
                PaymentSession = session.Id
            };
        }

        public bool IsPaymentSuccess(string sessionId)
        {
            var service = new SessionService();
            var checkoutSession = service.Get(sessionId);

            if (checkoutSession.PaymentStatus == "paid")
            {
                return true;
            }

            return false;
        }
    }
}
