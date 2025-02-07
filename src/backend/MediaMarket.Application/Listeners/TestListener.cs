using MediaMarket.Application.Bases;
using MediaMarket.Application.Contracts.Common;
using MediaMarket.Application.Contracts.Services;

namespace MediaMarket.Application.Listeners
{
    public class TestListener(ICategoryService categoryService, IBalanceService balanceService) : IListener, IShouldQueue
    {
        private readonly ICategoryService _categoryService = categoryService;
        private readonly IBalanceService _balanceService = balanceService;

        public async Task HandleAsync(EventBase @event)
        {
            var category = await _categoryService.GetAllCategories();
            throw new NotImplementedException();
        }
    }
}
