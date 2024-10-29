using MediaMarket.Application.Contracts.Services;
using MediaMarket.Application.DTO.Request.Tag;
using MediaMarket.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace MediaMarket.API.Controllers.Client
{
    [ApiController]
    public class TagController(ITagService tagService) : ApiBaseController
    {
        private readonly ITagService _tagService = tagService;

        [HttpGet(Router.TagRouting.Action.Index)]
        public async Task<IActionResult> Index()
        {
            var response = await _tagService.GetAll();
            return CustomResult(response);
        }

        [HttpPost(Router.TagRouting.Action.Create)]
        public async Task<IActionResult> Create([FromBody] CreateTagRequest request)
        {
            var response = await _tagService.Create(request);
            return CustomResult(response);
        }

        [HttpPut(Router.TagRouting.Action.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateTagRequest request)
        {
            var response = await _tagService.Update(id, request);
            return CustomResult(response);
        }

        [HttpDelete(Router.TagRouting.Action.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var response = await _tagService.Delete(id);
            return CustomResult(response);
        }
    }
}
