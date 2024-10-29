using Microsoft.AspNetCore.Mvc;

namespace MediaMarket.API.Controllers.Client
{
    [ApiController]
    public class ProductController : ApiBaseController
    {
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
