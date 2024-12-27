using MediaMarket.Application.Contracts.Services;
using MediaMarket.Application.DTO.Request.Product;
using MediaMarket.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace MediaMarket.API.Controllers.Client
{
    [ApiController]
    public class SearchController(ISearchService searchService) : ApiBaseController
    {
        private readonly ISearchService _searchService = searchService;

        [HttpGet(Router.SearchRouting.Action.SearchProduct)]
        public async Task<IActionResult> Search([FromQuery] SearchProductWithTypeRequest request)
        {
            var response = await _searchService.GetProductsSearchResult(request.Search);
            return CustomResult(response);
        }
    }
}
