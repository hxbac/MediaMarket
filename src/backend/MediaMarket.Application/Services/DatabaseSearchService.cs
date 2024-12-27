using MediaMarket.Application.Bases;
using MediaMarket.Application.Contracts.Repositories;
using MediaMarket.Application.Contracts.Services;
using MediaMarket.Application.DTO.Response.Product;
using MediaMarket.Domain.Entities;

namespace MediaMarket.Application.Services
{
    public class DatabaseSearchService(
        IProductRepository productRepository
    ) : BaseResponseHandler, ISearchService
    {
        private readonly IProductRepository _productRepository = productRepository;

        public async Task<BaseResponse<List<SearchProductResponse>>> GetProductsSearchResult(string search)
        {
            var listProductIds = await GetProductIdsByKeyword(search);
            var products = await _productRepository.GetListProductsByIds(listProductIds);

            var orderIds = listProductIds.Select((id, index) => new { id, index }).ToDictionary(x => x.id, x => x.index);
            products.OrderBy(x => orderIds[x.Id]);

            return Success(products);
        }

        public Task IndexProductAsync(Product product)
        {
            return Task.CompletedTask;
        }

        private async Task<IEnumerable<Guid>> GetProductIdsByKeyword(string keyword)
        {
            const int numberOfResult = 8;
            var result = new List<Guid>();

            var listProductIdsMatchByName = await SearchProductsByName(keyword);
            result.AddRange(listProductIdsMatchByName);
            if (result.Count >= numberOfResult)
            {
                return result;
            }

            var listProductIdsMatchByTag = await SearchProductsByTagName(keyword, result, numberOfResult - result.Count());
            result.AddRange(listProductIdsMatchByTag);
            if (result.Count >= numberOfResult)
            {
                return result;
            }

            var listProductIdsMatchByCategory = await SearchProductsByCategoryName(keyword, result, numberOfResult - result.Count());
            result.AddRange(listProductIdsMatchByCategory);
            return result;
        }

        private async Task<IEnumerable<Guid>> SearchProductsByCategoryName(string search, ICollection<Guid> excludedIds, int take)
        {
            return await _productRepository.GetProductIdsMatchCategoryName(search, excludedIds, take);
        }

        private async Task<IEnumerable<Guid>> SearchProductsByName(string search, int take = 8)
        {
            return await _productRepository.GetProductIdsMatchKeyword(search, take);
        }

        private async Task<IEnumerable<Guid>> SearchProductsByTagName(string search, ICollection<Guid> excludedIds, int take)
        {
            return await _productRepository.GetProductIdsMatchTagName(search, excludedIds, take);
        }
    }
}
