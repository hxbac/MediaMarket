using MediaMarket.Application.Contracts.Repositories;
using MediaMarket.Application.Contracts.Services;
using MediaMarket.Domain.Entities;
using MediaMarket.Domain.Enums;

namespace MediaMarket.Application.Services
{
    public class BalanceService(
        IUserRepository userRepository,
        IBalanceHistoryRepository balanceHistoryRepository
    ) : IBalanceService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IBalanceHistoryRepository _balanceHistoryRepository = balanceHistoryRepository;

        public async Task UpdateUserBalance(Guid? accountId, long amount, Guid transactionId, TransactionType transactionType, BalanceType balanceType)
        {
            if (accountId == null)
            {
                return;
            }

            var user = await _userRepository.FindByIdAsync(accountId ?? Guid.Empty);
            if (user == null)
            {
                return;
            }

            var newBalance = user.Balance;
            if (balanceType == BalanceType.TopUp)
            {
                newBalance += amount;
            }
            else
            {
                newBalance -= amount;
            }

            var balanceHistory = new BalanceHistory()
            {
                Id = Guid.NewGuid(),
                UserId = accountId ?? Guid.Empty,
                PreviousBalance = user.Balance,
                NewBalance = newBalance,
                TransactionId = transactionId,
                TransactionType = transactionType,
                CreatedOn = DateTime.UtcNow,
            };
            user.Balance = balanceHistory.NewBalance;
            await _balanceHistoryRepository.AddAsync(balanceHistory);

            await _userRepository.SaveChangesAsync();
        }
    }
}
