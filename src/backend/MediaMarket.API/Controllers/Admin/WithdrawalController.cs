using Microsoft.AspNetCore.Mvc;

namespace MediaMarket.API.Controllers.Admin
{
    public class WithdrawalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
