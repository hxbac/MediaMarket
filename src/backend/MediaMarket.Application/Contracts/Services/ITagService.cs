using MediaMarket.Application.Bases;
using MediaMarket.Application.DTO.Request.Tag;
using MediaMarket.Application.DTO.Response.Tag;

namespace MediaMarket.Application.Contracts.Services
{
    public interface ITagService
    {
        Task<BaseResponse<IEnumerable<TagResponse>>> GetAll();
        Task<BaseResponse<TagResponse>> Create(CreateTagRequest request);
        Task<BaseResponse<TagResponse>> Update(Guid id, UpdateTagRequest request);
        Task<BaseResponse<Guid>> Delete(Guid id);
    }
}
