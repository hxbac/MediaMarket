using MediaMarket.Application.Bases;
using MediaMarket.Application.DTO.Response.Product;

namespace MediaMarket.Application.Contracts.Services
{
    public interface ISearchService
    {
        Task<BaseResponse<List<SearchProductResponse>>> GetProductsSearchResult(string search);
    }
}
