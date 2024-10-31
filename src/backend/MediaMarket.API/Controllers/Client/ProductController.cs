using MediaMarket.Application.Contracts.Services;
using MediaMarket.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace MediaMarket.API.Controllers.Client
{
    [ApiController]
    public class ProductController(IProductService productService) : ApiBaseController
    {
        private readonly IProductService _productService = productService;

        [HttpGet(Router.ProductRouting.Action.Show)]
        public async Task<IActionResult> Show(Guid id)
        {
            var response = await _productService.GetProductDetail(id);
            return CustomResult(response);
        }
    }
}
