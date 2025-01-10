using MediaMarket.Domain.Enums;

namespace MediaMarket.Application.Contracts.Services
{
    public interface IBalanceService
    {
        Task UpdateUserBalance(Guid accountId, long amount, Guid transactionId, TransactionType transactionType, BalanceType balanceType);
    }
}
