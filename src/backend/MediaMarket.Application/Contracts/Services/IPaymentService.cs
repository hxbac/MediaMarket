using MediaMarket.Application.DTO.Payment;
using MediaMarket.Application.DTO.Product;

namespace MediaMarket.Application.Contracts.Services
{
    public interface IPaymentService
    {
        CreatePaymentDTO Create(ProductLatestVersionDTO product);
        bool IsPaymentSuccess(string sessionId);
    }
}
