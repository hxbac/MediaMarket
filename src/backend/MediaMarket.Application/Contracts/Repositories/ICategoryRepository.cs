using AutoMapper;
using MediaMarket.Application.Bases;
using MediaMarket.Domain.Entities;

namespace MediaMarket.Application.Contracts.Repositories
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<IEnumerable<Product>> GetProductsByCategoryId(Guid categoryId);
        Task<PaginatedResult<Category>> GetListPagination(IMapper mapper);
    }
}
