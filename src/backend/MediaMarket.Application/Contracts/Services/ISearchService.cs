using MediaMarket.Application.Bases;
using MediaMarket.Application.DTO.Response.Product;
using MediaMarket.Domain.Enums;

namespace MediaMarket.Application.Contracts.Services
{
    public interface ISearchService
    {
        Task<BaseResponse<ICollection<SearchProductResponse>>> GetProductsSearchResult(string search, ProductType productType);
    }
}
