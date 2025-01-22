using MediaMarket.Application.Bases;
using MediaMarket.Application.Contracts.Common;
using MediaMarket.Application.Contracts.Services;

namespace MediaMarket.Application.Listeners
{
    public class TestListener : IListener
    {
        private readonly ICategoryService _categoryService;
        private readonly IBalanceService _balanceService;

        public TestListener(ICategoryService categoryService, IBalanceService balanceService)
        {
            _categoryService = categoryService;
            _balanceService = balanceService;
        }

        public async Task HandleAsync(EventBase @event)
        {
            var category = await _categoryService.GetAllCategories();
            throw new NotImplementedException();
        }
    }
}
