using MediaMarket.Application.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace MediaMarket.API.Controllers.Client
{
    [ApiController]
    public class SearchController(ISearchService searchService) : ApiBaseController
    {
        private readonly ISearchService _searchService = searchService;

        [HttpGet("/test")]
        public Task<IActionResult> Search()
        {

        }
    }
}
