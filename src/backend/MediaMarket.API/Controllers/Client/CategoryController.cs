using MediaMarket.Application.Contracts.Services;
using MediaMarket.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace MediaMarket.API.Controllers.Client
{
    [ApiController]
    public class CategoryController(ICategoryService categoryService) : ApiBaseController
    {
        private readonly ICategoryService _categoryService = categoryService;

        [HttpGet(Router.CategoryRouting.Action.Index)]
        public async Task<IActionResult> GetAll()
        {
            var response = await _categoryService.GetAllCategories();
            return CustomResult(response);
        }
    }
}
