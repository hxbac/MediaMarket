using MediaMarket.Application.Contracts.Services;
using MediaMarket.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace MediaMarket.API.Controllers.Client
{
    [ApiController]
    public class FileController(IFileService _fileService) : ApiBaseController
    {
        [HttpPost(Router.FileRouting.Action.UploadSingle)]
        public async Task<IActionResult> UploadSingle(IFormFile file)
        {
            var response = await _fileService.UploadSingleFile(file);
            foreach (var item in response.Data)
            {
                item.Url = $"{Request.Scheme}://{Request.Host}/{item.Path}";
            }

            return CustomResult(response);
        }
    }
}
