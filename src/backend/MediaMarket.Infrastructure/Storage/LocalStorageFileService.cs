using MediaMarket.Application.Bases;
using MediaMarket.Application.Contracts.Services;
using MediaMarket.Application.DTO.Response.File;
using Microsoft.AspNetCore.Http;

namespace MediaMarket.Infrastructure.Storage
{
    public class LocalStorageFileService : BaseResponseHandler, IFileService
    {
        public async Task<BaseResponse<List<UploadSingleFileResponse>>> UploadSingleFile(IFormFile file)
        {
            var pathDateUpload = DateTime.Now.Year + "/" + DateTime.Now.Month;
            var publicPath = "storage/uploads/public/" + pathDateUpload;
            var uploadsDirectory = Path.Combine(Directory.GetCurrentDirectory(), publicPath);
            Directory.CreateDirectory(uploadsDirectory);

            var fileName = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString() + file.FileName;
            var filePath = Path.Combine(uploadsDirectory, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var files = new List<UploadSingleFileResponse>();
            files.Add(new UploadSingleFileResponse
            {
                FileName = fileName,
                Path = "storage/" + pathDateUpload + "/" + fileName,
            });

            return Success(files);
        }
    }
}
