using MediaMarket.Application.Configs;
using MediaMarket.Application.Contracts.Services;
using MediaMarket.Application.DTO.Payment;
using MediaMarket.Application.DTO.Product;
using MediaMarket.Domain.Entities;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;

namespace MediaMarket.Infrastructure.Payment
{
    public class StripePaymentService : IPaymentService
    {
        public StripePaymentService(
            IOptionsMonitor<StripeConfig> stripeConfig
        )
        {
            StripeConfiguration.ApiKey = stripeConfig.CurrentValue.SecretKey;
        }

        public async Task AddDebitCardForUser(User user, string cardToken)
        {
            var externalAccountService = new AccountService();

            var options = new AccountUpdateOptions
            {
                ExternalAccount = cardToken,
            };

            await externalAccountService.UpdateAsync(user.StripeAccountId, options);
        }

        public async Task<CreatePaymentDTO> Create(ProductLatestVersionDTO product)
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
            Session session = await service.CreateAsync(options);

            return new CreatePaymentDTO()
            {
                RedirectUrl = session.Url,
                PaymentSession = session.Id
            };
        }

        public async Task<string> CreateAccount(User user)
        {
            var accountService = new AccountService();

            var accountOptions = new AccountCreateOptions
            {
                Type = "express",
                Email = user.Email,
            };

            var account = await accountService.CreateAsync(accountOptions);

            return account.Id;
        }

        public async Task<string> CreateRequestPayout(long amount, string accountId)
        {
            var payoutService = new PayoutService();

            var options = new PayoutCreateOptions
            {
                Amount = amount,
                Currency = "VND",
                Method = "instant",
            };

            var payout = await payoutService.CreateAsync(options, new RequestOptions
            {
                StripeAccount = accountId
            });

            return payout.Id;
        }

        public async Task<bool> IsPaymentSuccess(string sessionId)
        {
            var service = new SessionService();
            var checkoutSession = await service.GetAsync(sessionId);

            if (checkoutSession.PaymentStatus == "paid")
            {
                return true;
            }

            return false;
        }
    }
}
