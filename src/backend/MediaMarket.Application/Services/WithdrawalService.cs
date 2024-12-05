using AutoMapper;
using MediaMarket.Application.Bases;
using MediaMarket.Application.Contracts.Common;
using MediaMarket.Application.Contracts.Repositories;
using MediaMarket.Application.Contracts.Services;
using MediaMarket.Application.DTO.Request.Withdrawal;
using MediaMarket.Application.DTO.Response.Withdrawal;
using MediaMarket.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace MediaMarket.Application.Services
{
    public class WithdrawalService(
        IUser user,
        IMapper mapper,
        UserManager<User> userManager,
        IPaymentService paymentService,
        IWithdrawalRepository withdrawalRepository
    ) : BaseResponseHandler, IWithdrawalService
    {
        private readonly IUser _user = user;
        private readonly IMapper _mapper = mapper;
        private readonly UserManager<User> _userManager = userManager;
        private readonly IPaymentService _paymentService = paymentService;
        private readonly IWithdrawalRepository _withdrawalRepository = withdrawalRepository;

        public async Task<BaseResponse<CreateWithdrawalResponse>> CreateRequest(CreateWithdrawalRequest request)
        {
            var user = await _userManager.FindByIdAsync(_user.Id.ToString());

            var accountId = user.StripeAccountId;
            if (accountId == null)
            {
                accountId = await _paymentService.CreateAccount(user);
            }

            await _paymentService.AddDebitCardForUser(user, request.CardToken);

            var dataWithdrawal = new Withdrawal()
            {
                Id = Guid.NewGuid(),
                AccountId = accountId,
                Amount = request.Amount,
                WithdrawalStatus = Domain.Enums.WithdrawalStatus.Pending
            };
            await _withdrawalRepository.AddAsync(dataWithdrawal);
            await _withdrawalRepository.SaveChangesAsync();

            return Success(_mapper.Map<CreateWithdrawalResponse>(dataWithdrawal));
        }

        public async Task<BaseResponse<PaginatedResult<WithdrawlCurrentUserResponse>>> GetListWithdrawalForCurrentUser(GetListWithdrawalCurrentUserRequest request)
        {
            var data = await _withdrawalRepository.GetListPaginatedForUser(request, _user.Id);
            return Success(data);
        }
    }
}
