﻿using MediaMarket.Application.Bases;
using MediaMarket.Application.DTO.Request.Product;
using MediaMarket.Application.DTO.Response.Product;

namespace MediaMarket.Application.Contracts.Services
{
    public interface IProductService
    {
        Task<BaseResponse<CreateProductResponse>> CreateProduct(CreateProductRequest request, Guid UserIdCreate);
        Task<BaseResponse<IEnumerable<ProductLatestResponse>>> GetLatestProductsOfUser(LatestProductOfUserRequest request);
        Task<BaseResponse<IEnumerable<ProductHomePageResponse>>> GetListProductsHomePageByCategory(Guid categoryId);
        Task<BaseResponse<IEnumerable<ProductLatestResponse>>> GetMyLatestProducts();
        Task<BaseResponse<PaginatedResult<ProductUserResponse>>> GetMyProductsPaginated(GetProductListRequest request);
        Task<BaseResponse<ProductCheckoutResponse>> GetProductCheckoutInfo(ProductCheckoutRequest request);
        Task<BaseResponse<ProductDetailResponse>> GetProductDetail(string slug);
    }
}
