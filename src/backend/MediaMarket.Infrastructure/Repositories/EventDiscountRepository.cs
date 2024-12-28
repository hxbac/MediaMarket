using MediaMarket.Application.Bases;
using MediaMarket.Application.Contracts.Repositories;
using MediaMarket.Application.DTO.Request.EventDiscount;
using MediaMarket.Application.DTO.Response.EventDiscount;
using MediaMarket.Domain.Entities;
using MediaMarket.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MediaMarket.Infrastructure.Repositories
{
    public class EventDiscountRepository : BaseRepository<EventDiscount>, IEventDiscountRepository
    {
        public EventDiscountRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Task<List<EventDiscount>> GetEventDiscountActive()
        {
            var currentDate = DateTime.Now;
            return _model
                .Where(d => d.StartDate <= currentDate && d.EndDate >= currentDate)
                .Select(d => new EventDiscount()
                {
                    Id = d.Id,
                    Type = d.Type,
                    Name = d.Name,
                    Value = d.Value,
                    EndDate = d.EndDate,
                })
                .ToListAsync();
        }

        public Task<PaginatedResult<EventDiscountManageResponse>> GetListPaginated(GetListPaginatedRequest request)
        {
            return _model
                .OrderByDescending(x => x.StartDate)
                .Select(x => new EventDiscountManageResponse()
                {
                    Id = x.Id,
                    Description = x.Description,
                    Name = x.Name,
                    Type = x.Type,
                    Value = x.Value,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                }).ToPaginatedListAsync(request.Page, request.PageSize);
        }
    }
}
