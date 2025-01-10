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

        public async Task UpdateUserBalance(Guid accountId, long amount, Guid transactionId, TransactionType transactionType, BalanceType balanceType)
        {
            using var transaction = await _userRepository.BeginTransaction(System.Data.IsolationLevel.RepeatableRead);
            try
            {
                var user = await _userRepository.FindByIdAsync(accountId);
                if (user == null)
                {
                    await transaction.CommitAsync();
                    return;
                }

                var newBalance = balanceType == BalanceType.TopUp ? user.Balance + amount : user.Balance - amount;
                user.Balance = newBalance;

                var balanceHistory = new BalanceHistory()
                {
                    Id = Guid.NewGuid(),
                    UserId = accountId,
                    PreviousBalance = user.Balance,
                    NewBalance = newBalance,
                    TransactionId = transactionId,
                    TransactionType = transactionType,
                    CreatedOn = DateTime.UtcNow,
                };
                await _balanceHistoryRepository.AddAsync(balanceHistory);

                await _userRepository.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
