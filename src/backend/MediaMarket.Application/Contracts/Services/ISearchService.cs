using MediaMarket.Domain.Enums;

namespace MediaMarket.Application.Contracts.Services
{
    public interface ISearchService
    {
        Task<ICollection<Guid>> SearchProductsByCategoryName(string search, ProductType productType);
        Task<ICollection<Guid>> SearchProductsByTagName(string search, ProductType productType);
        Task<ICollection<Guid>> SearchProductsByName(string search, ProductType productType);
    }
}
