using MediaMarket.Application.Bases;
using MediaMarket.Application.DTO.Response.Product;

namespace MediaMarket.Application.Contracts.Services
{
    public interface IProductService
    {
        Task<BaseResponse<IEnumerable<ProductHomePageResponse>>> GetListProductsHomePageByCategory(Guid categoryId);
        Task<BaseResponse<ProductDetailResponse>> GetProductDetail(Guid productId);
    }
}
