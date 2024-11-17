using MediaMarket.Application.Bases;
using MediaMarket.Application.DTO.Response.Category;
using MediaMarket.Domain.Entities;

namespace MediaMarket.Application.Contracts.Services
{
    public interface ICategoryService
    {
        Task<BaseResponse<IEnumerable<CategoryResponse>>> GetAllCategories();
        Task<BaseResponse<PaginatedResult<Category>>> GetListPagination();
    }
}
