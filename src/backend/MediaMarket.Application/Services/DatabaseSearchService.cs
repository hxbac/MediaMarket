using MediaMarket.Application.Contracts.Repositories;
using MediaMarket.Application.Contracts.Services;
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

        public async Task<ICollection<Guid>> SearchProductsByCategoryName(string search, ProductType productType)
        {
            return await _productRepository.GetProductIdsMatchTagName(search, productType, 8);
        }

        public Task<ICollection<Guid>> SearchProductsByName(string search, ProductType productType)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Guid>> SearchProductsByTagName(string search, ProductType productType)
        {
            throw new NotImplementedException();
        }
    }
}
