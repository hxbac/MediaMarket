﻿using MediaMarket.Application.Contracts.Services;
using MediaMarket.Application.DTO.Payment;
using MediaMarket.Application.DTO.Product;
using MediaMarket.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Stripe;
using Stripe.Checkout;

namespace MediaMarket.Infrastructure.Payment
{
    public class StripePaymentService : IPaymentService
    {
        private readonly UserManager<User> _userManager;

        public StripePaymentService(UserManager<User> userManager)
        {
            _userManager = userManager;
            StripeConfiguration.ApiKey = "sk_test_51QJpfyGOIY1d15fJqpCqCaqKVpiyd54j6YRs45TRMDBGfTgJAIwcazenx2CCQqt8WrQi9uc30WOE1yEDlZrpEh6S00Auquaj9H";
        }

        public async Task<string> AddDebitCardForUser(User user, string cardToken)
        {
            var accountId = user.StripeAccountId;
            if (accountId == null)
            {
                accountId = await CreateAccount(user);
            }

            var externalAccountService = new AccountService();

            var options = new AccountUpdateOptions
            {
                ExternalAccount = cardToken,
            };

            var account = await externalAccountService.UpdateAsync(accountId, options);
            return account.Id;
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
            user.StripeAccountId = account.Id;
            await _userManager.UpdateAsync(user);

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
