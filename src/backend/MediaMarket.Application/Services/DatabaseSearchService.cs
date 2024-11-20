using MediaMarket.Application.Bases;
using MediaMarket.Application.Contracts.Repositories;
using MediaMarket.Application.Contracts.Services;
using MediaMarket.Application.DTO.Response.Product;
using MediaMarket.Domain.Enums;

namespace MediaMarket.Application.Services
{
    public class DatabaseSearchService(
        ICategoryRepository categoryRepository,
        ITagRepository tagRepository,
        IProductRepository productRepository
    ) : ISearchService
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly ITagRepository _tagRepository = tagRepository;
        private readonly IProductRepository _productRepository = productRepository;

        public async Task<BaseResponse<ICollection<SearchProductResponse>>> GetProductsSearchResult(string search, ProductType productType)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<SearchProductResponse>> SearchProductsByCategoryName(string search, ProductType productType)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<SearchProductResponse>> SearchProductsByName(string search, ProductType productType)
        {
            throw new NotImplementedException();
        }

        private async Task<ICollection<SearchProductResponse>> SearchProductsByTagName(string search, ProductType productType, ICollection<Guid> excludedIds, int take)
        {
            return await _productRepository.GetProductIdsMatchTagName(search, productType, excludedIds, take);
        }
    }
}
