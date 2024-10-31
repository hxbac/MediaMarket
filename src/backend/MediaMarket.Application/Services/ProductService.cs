using MediaMarket.Application.Bases;
using MediaMarket.Application.Contracts.Repositories;
using MediaMarket.Application.Contracts.Services;
using MediaMarket.Application.DTO.Response.Product;

namespace MediaMarket.Application.Services
{
    public class ProductService(IProductRepository _productRepository) : BaseResponseHandler, IProductService
    {
        public async Task<BaseResponse<ProductDetailResponse>> GetProductDetail(Guid productId)
        {
            var product = await _productRepository.GetProductActiveWithRelationship(productId);
            return Success(product);
        }
    }
}
