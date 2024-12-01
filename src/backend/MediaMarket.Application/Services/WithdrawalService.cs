using MediaMarket.Application.Bases;
using MediaMarket.Application.Contracts.Common;
using MediaMarket.Application.Contracts.Services;
using MediaMarket.Application.DTO.Request.Withdrawal;
using MediaMarket.Application.DTO.Response.Withdrawal;
using MediaMarket.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace MediaMarket.Application.Services
{
    public class WithdrawalService(
        IUser user,
        UserManager<User> userManager,
        IPaymentService paymentService
    ) : IWithdrawalService
    {
        private readonly IUser _user = user;
        private readonly UserManager<User> _userManager = userManager;
        private readonly IPaymentService _paymentService = paymentService;

        public async Task<BaseResponse<CreateWithdrawalResponse>> CreateRequest(CreateWithdrawalRequest request)
        {
            var user = await _userManager.FindByIdAsync(_user.Id.ToString());

            var accountId = user.StripeAccountId;
            if (accountId == null)
            {
                accountId = await _paymentService.CreateAccount(user);
            }

            await _paymentService.AddDebitCardForUser(user, request.CardToken);
            var payoutId = await _paymentService.CreateRequestPayout(request.Amount, accountId);
            throw new NotImplementedException();
        }
    }
}
