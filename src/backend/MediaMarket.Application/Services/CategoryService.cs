using AutoMapper;
using MediaMarket.Application.Bases;
using MediaMarket.Application.Contracts.Repositories;
using MediaMarket.Application.Contracts.Services;
using MediaMarket.Application.DTO.Response.Category;

namespace MediaMarket.Application.Services
{
    public class CategoryService(ICategoryRepository _categoryRepository, IMapper _mapper) : BaseResponseHandler, ICategoryService
    {
        public async Task<BaseResponse<IEnumerable<CategoryResponse>>> GetAllCategories()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return Success(_mapper.Map<IEnumerable<CategoryResponse>>(categories));
        }
    }
}
