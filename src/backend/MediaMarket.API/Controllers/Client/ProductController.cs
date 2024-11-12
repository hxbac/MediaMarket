﻿using MediaMarket.Application.Contracts.Services;
using MediaMarket.Application.DTO.Request.Product;
using MediaMarket.Domain.Common;
using MediaMarket.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MediaMarket.API.Controllers.Client
{
    [ApiController]
    public class ProductController(IProductService productService) : ApiBaseController
    {
        private readonly IProductService _productService = productService;

        [HttpGet(Router.ProductRouting.Action.GetProductsHomePage)]
        public async Task<IActionResult> GetProductsHomePage([FromQuery] Guid categoryId)
        {
            var response = await _productService.GetListProductsHomePageByCategory(categoryId);
            return CustomResult(response);
        }

        [HttpGet(Router.ProductRouting.Action.Show)]
        public async Task<IActionResult> Show(string slug)
        {
            var response = await _productService.GetProductDetail(slug);
            return CustomResult(response);
        }

        [Authorize]
        [HttpPost(Router.ProductRouting.Action.Create)]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
        {
            var subJwt = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isGuidValid = Guid.TryParse(subJwt, out var userId);
            if (!isGuidValid)
            {
                throw new SecurityTokenException();
            }

            var response = await _productService.CreateProduct(request, userId);
            return CustomResult(response);
        }
    }
}
