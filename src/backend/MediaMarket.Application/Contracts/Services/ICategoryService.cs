using MediaMarket.Application.Bases;
using MediaMarket.Application.DTO.Response.Category;

namespace MediaMarket.Application.Contracts.Services
{
    public interface ICategoryService
    {
        public Task<BaseResponse<IEnumerable<CategoryResponse>>> GetAllCategories();
    }
}
