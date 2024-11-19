using MediaMarket.Application.Bases;
using MediaMarket.Application.DTO.Product;
using MediaMarket.Application.DTO.Response.Product;
using MediaMarket.Domain.Entities;
using MediaMarket.Domain.Enums;

namespace MediaMarket.Application.Contracts.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<PaginatedResult<ProductUserResponse>> GetListProductsForUser(Guid userId, ProductType productType, string? name, int page, int pageSize);
        Task<ProductDetailResponse> GetProductActiveWithRelationship(string slug);
        Task<ProductLatestVersionDTO> GetProductWithLatestVersion(string slug);
        Task<IEnumerable<Product>> GetProductsLatest(Guid userId);
        Task<ICollection<Guid>> GetProductIdsMatchTagName(string tagName, ProductType productType, int take);
    }
}
