using MediaMarket.Application.Bases;
using MediaMarket.Application.Contracts.Repositories;
using MediaMarket.Application.DTO.Request.User;
using MediaMarket.Application.DTO.Response.User;
using MediaMarket.Domain.Entities;
using MediaMarket.Infrastructure.Data;

namespace MediaMarket.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Task<PaginatedResult<UserManageResponse>> GetListPaginated(GetListUserPaginatedRequest request)
        {
            return _model.Select(x => new UserManageResponse()
            {
                Id = x.Id,
                Email = x.Email,
                UserName = x.UserName,
                Name = x.Name,
                Balance = x.Balance,
                LockoutEnabled = x.LockoutEnabled,
                NumberOrder = x.Orders.Count(),
                NumberProduct = x.Products.Count(),

            }).ToPaginatedListAsync(request.Page, request.PageSize);
        }
    }
}
