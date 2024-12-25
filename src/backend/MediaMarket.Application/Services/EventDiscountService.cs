using AutoMapper;
using MediaMarket.Application.Bases;
using MediaMarket.Application.Contracts.Repositories;
using MediaMarket.Application.Contracts.Services;
using MediaMarket.Application.DTO.Request.EventDiscount;
using MediaMarket.Application.DTO.Response.EventDiscount;
using MediaMarket.Domain.Entities;

namespace MediaMarket.Application.Services
{
    public class EventDiscountService(
        IEventDiscountRepository eventDiscountRepository,
        IMapper mapper
    ) : BaseResponseHandler, IEventDiscountService
    {
        private readonly IEventDiscountRepository _eventDiscountRepository = eventDiscountRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<EventDiscountManageResponse>> Create(CreateDiscountRequest request)
        {
            if (!DateTime.TryParse(request.StartDate, out DateTime startDate) || !DateTime.TryParse(request.EndDate, out DateTime endDate))
            {
                return NotFound<EventDiscountManageResponse>("Định dạng thời gian không hợp lệ");
            }

            var discount = new EventDiscount()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                DiscountMinValue = request.DiscountMinValue,
                StartDate = startDate,
                EndDate = endDate,
                Type = request.Type,
                Value = request.Value,
            };
            await _eventDiscountRepository.AddAsync(discount);
            await _eventDiscountRepository.SaveChangesAsync();
            return Success(_mapper.Map<EventDiscountManageResponse>(discount));
        }

        public async Task<BaseResponse<PaginatedResult<EventDiscountManageResponse>>> GetListPaginated(GetListPaginatedRequest request)
        {
            var result = await _eventDiscountRepository.GetListPaginated(request);
            return Success(result);
        }
    }
}
