using MediaMarket.Application.DTO.Payment;
using MediaMarket.Application.DTO.Product;
using MediaMarket.Domain.Entities;

namespace MediaMarket.Application.Contracts.Services
{
    public interface IPaymentService
    {
        Task<CreatePaymentDTO> Create(ProductLatestVersionDTO product);
        Task<bool> IsPaymentSuccess(string sessionId);
        Task<string> AddDebitCardForUser(User user, string cardToken);
        Task<string> CreateRequestPayout(long amount, string accountId);
    }
}
