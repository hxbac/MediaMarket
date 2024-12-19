using MediaMarket.Application.Contracts.Services;
using MediaMarket.Application.DTO.Request.Product;
using MediaMarket.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace MediaMarket.API.Controllers.Worker
{
    [ApiController]
    public class ProductController(IProductService productService) : ApiBaseController
    {
        private readonly IProductService _productService = productService;

        [HttpPut(Router.Worker.ProductRouting.Action.UpdateContentStatus)]
        public async Task<IActionResult> Index([FromBody] UpdateContentStatusRequest request)
        {
            var response = await _productService.UpdateProductContentStatus(request);
            return CustomResult(response);
        }
    }
}
