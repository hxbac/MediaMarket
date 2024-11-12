using MediaMarket.Application.Contracts.Services;
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

        public string Create()
        {
            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmount = 2000,
                    Currency = "usd",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                    Name = "T-shirt",
                    },
                },
                    Quantity = 1,
                },
            },
                Mode = "payment",
                SuccessUrl = "http://localhost:4242/success",
                CancelUrl = "http://localhost:4242/cancel",
            };

            var service = new SessionService();
            Session session = service.Create(options);

            return session.Url;
        }

        public void FulfillCheckout(String sessionId)
        {

            // TODO: Make this function safe to run multiple times,
            // even concurrently, with the same session ID

            // TODO: Make sure fulfillment hasn't already been
            // peformed for this Checkout Session

            // Retrieve the Checkout Session from the API with line_items expanded
            var options = new SessionGetOptions
            {
                Expand = new List<string> { "line_items" },
            };

            var service = new SessionService();
            var checkoutSession = service.Get(sessionId, options);

            // Check the Checkout Session's payment_status property
            // to determine if fulfillment should be peformed
            if (checkoutSession.PaymentStatus != "unpaid")
            {
                // TODO: Perform fulfillment of the line items

                // TODO: Record/save fulfillment status for this
                // Checkout Session
            }
        }
    }
}
