using MediaMarket.Application.DTO.Product;
using MediaMarket.Application.DTO.Response.Product;
using MediaMarket.Domain.Entities;

namespace MediaMarket.Application.Contracts.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<ProductDetailResponse> GetProductActiveWithRelationship(string slug);
        Task<ProductLatestVersionDTO> GetProductWithLatestVersion(string slug);
    }
}
