using MediaMarket.Application.Bases;
using MediaMarket.Application.Contracts.Repositories;
using MediaMarket.Application.Contracts.Services;
using MediaMarket.Application.DTO.Response.Product;
using MediaMarket.Domain.Enums;

namespace MediaMarket.Application.Services
{
    public class DatabaseSearchService(
        IProductRepository productRepository
    ) : BaseResponseHandler, ISearchService
    {
        private readonly IProductRepository _productRepository = productRepository;

        public async Task<BaseResponse<IEnumerable<SearchProductResponse>>> GetProductsSearchResult(string search, ProductType productType)
        {
            var listProductIds = await GetProductIdsByKeyword(search, productType);
            var products = await _productRepository.GetListProductsByIds(listProductIds);

            var orderIds = listProductIds.Select((id, index) => new { id, index }).ToDictionary(x => x.id, x => x.index);
            products.OrderBy(x => orderIds[x.Id]);

            return Success(products);
        }

        private async Task<IEnumerable<Guid>> GetProductIdsByKeyword(string keyword, ProductType productType)
        {
            const int numberOfResult = 8;
            var result = new List<Guid>();

            var listProductIdsMatchByName = await SearchProductsByName(keyword, productType);
            result.AddRange(listProductIdsMatchByName);
            if (result.Count >= numberOfResult)
            {
                return result;
            }

            var listProductIdsMatchByTag = await SearchProductsByTagName(keyword, productType, result, numberOfResult - result.Count());
            result.AddRange(listProductIdsMatchByTag);
            if (result.Count >= numberOfResult)
            {
                return result;
            }

            var listProductIdsMatchByCategory = await SearchProductsByCategoryName(keyword, productType, result, numberOfResult - result.Count());
            result.AddRange(listProductIdsMatchByCategory);
            return result;
        }

        private async Task<IEnumerable<Guid>> SearchProductsByCategoryName(string search, ProductType productType, ICollection<Guid> excludedIds, int take)
        {
            return await _productRepository.GetProductIdsMatchCategoryName(search, productType, excludedIds, take);
        }

        private async Task<IEnumerable<Guid>> SearchProductsByName(string search, ProductType productType, int take = 8)
        {
            return await _productRepository.GetProductIdsMatchKeyword(search, productType, take);
        }

        private async Task<IEnumerable<Guid>> SearchProductsByTagName(string search, ProductType productType, ICollection<Guid> excludedIds, int take)
        {
            return await _productRepository.GetProductIdsMatchTagName(search, productType, excludedIds, take);
        }
    }
}
