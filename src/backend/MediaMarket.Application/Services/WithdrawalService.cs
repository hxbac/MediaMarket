using AutoMapper;
using MediaMarket.Application.Bases;
using MediaMarket.Application.Contracts.Common;
using MediaMarket.Application.Contracts.Repositories;
using MediaMarket.Application.Contracts.Services;
using MediaMarket.Application.DTO.Request.Withdrawal;
using MediaMarket.Application.DTO.Response.Withdrawal;
using MediaMarket.Domain.Entities;
using MediaMarket.Domain.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace MediaMarket.Application.Services
{
    public class WithdrawalService(
        IUser user,
        IMapper mapper,
        UserManager<User> userManager,
        IPaymentService paymentService,
        IWithdrawalRepository withdrawalRepository,
        IBalanceService balanceService
    ) : BaseResponseHandler, IWithdrawalService
    {
        private readonly IUser _user = user;
        private readonly IMapper _mapper = mapper;
        private readonly UserManager<User> _userManager = userManager;
        private readonly IPaymentService _paymentService = paymentService;
        private readonly IWithdrawalRepository _withdrawalRepository = withdrawalRepository;
        private readonly IBalanceService _balanceService = balanceService;

        public async Task<BaseResponse<object>> ApprovalRequest(Guid id, ApprovalRequest request)
        {
            var withdrawal = await _withdrawalRepository.FindByIdAsync(id);
            if (withdrawal.WithdrawalStatus != Domain.Enums.WithdrawalStatus.Pending)
            {
                //Todo 
            }

            withdrawal.WithdrawalStatus = request.Status;
            withdrawal.Note = request.Note;
            withdrawal.ProcessedAt = DateTime.UtcNow;
            await _withdrawalRepository.SaveChangesAsync();

            return Success<object>(null);
        }

        public async Task<BaseResponse<CreateWithdrawalResponse>> CreateRequest(CreateWithdrawalRequest request)
        {
            var user = await _userManager.FindByIdAsync(_user.Id.ToString());
            if (user == null)
            {
                throw new EntityNotFoundException();
            }

            var accountId = user.StripeAccountId;
            if (accountId == null)
            {
                accountId = await _paymentService.CreateAccount(user);
                user.StripeAccountId = accountId;
                await _userManager.UpdateAsync(user);
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
            await _balanceService.UpdateUserBalance(
                user.Id,
                dataWithdrawal.Amount,
                dataWithdrawal.Id,
                Domain.Enums.TransactionType.Withdrawal,
                Domain.Enums.BalanceType.Reduce
            );
            await _withdrawalRepository.SaveChangesAsync();

            return Success(_mapper.Map<CreateWithdrawalResponse>(dataWithdrawal));
        }

        public async Task<BaseResponse<PaginatedResult<WithdrawalManageResponse>>> GetListPaginated(GetListWithdrawalPaginatedRequest request)
        {
            var data = await _withdrawalRepository.GetListPaginated(request);
            return Success(data);
        }

        public async Task<BaseResponse<PaginatedResult<WithdrawlCurrentUserResponse>>> GetListWithdrawalForCurrentUser(GetListWithdrawalCurrentUserRequest request)
        {
            var data = await _withdrawalRepository.GetListPaginatedForUser(request, _user.Id);
            return Success(data);
        }
    }
}
