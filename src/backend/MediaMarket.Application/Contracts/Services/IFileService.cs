using MediaMarket.Application.Bases;
using MediaMarket.Application.DTO.Response.File;
using Microsoft.AspNetCore.Http;

namespace MediaMarket.Application.Contracts.Services
{
    public interface IFileService
    {
        Task<BaseResponse<List<UploadSingleFileResponse>>> UploadSingleFile(IFormFile file);
    }
}
