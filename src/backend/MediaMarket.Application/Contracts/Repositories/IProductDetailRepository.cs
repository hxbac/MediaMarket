using MediaMarket.Domain.Entities;

namespace MediaMarket.Application.Contracts.Repositories
{
    public interface IProductDetailRepository : IBaseRepository<ProductDetail>
    {
        Task<ProductDetail> GetLatestVersion(Guid ProductId);
    }
}
