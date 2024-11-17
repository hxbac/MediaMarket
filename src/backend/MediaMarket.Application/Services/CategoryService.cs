using AutoMapper;
using MediaMarket.Application.Bases;
using MediaMarket.Application.Contracts.Repositories;
using MediaMarket.Application.Contracts.Services;
using MediaMarket.Application.DTO.Response.Category;
using MediaMarket.Domain.Entities;

namespace MediaMarket.Application.Services
{
    public class CategoryService(
        ICategoryRepository categoryRepository,
        IMapper mapper
    ) : BaseResponseHandler, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<IEnumerable<CategoryResponse>>> GetAllCategories()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return Success(_mapper.Map<IEnumerable<CategoryResponse>>(categories));
        }

        public async Task<BaseResponse<PaginatedResult<Category>>> GetListPagination()
        {
            var result = await _categoryRepository.GetListPagination(mapper);
            return Success(result);
        }
    }
}
