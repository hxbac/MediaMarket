using MediaMarket.Application.Bases;
using MediaMarket.Application.DTO.Response.Product;
using MediaMarket.Domain.Entities;

namespace MediaMarket.Application.Contracts.Services
{
    public interface ISearchService
    {
        Task<BaseResponse<List<SearchProductResponse>>> GetProductsSearchResult(string search);
        Task IndexProductAsync(Product product);
    }
}
