using MediaMarket.Application.Contracts.Services;
using MediaMarket.Application.DTO.GenerativeContentResponse.Product;
using Microsoft.AspNetCore.Mvc;

namespace MediaMarket.API.Controllers.Admin
{
    [ApiController]
    public class TestController(IGenerativeAIService service) : ApiBaseController
    {
        private readonly IGenerativeAIService _userService = service;

        [HttpGet("/test")]
        public async Task<IActionResult> Index()
        {
            var response = await _userService.GenerateContentAsync<EnhanceProductInformationDTO>("", new Dictionary<string, string>()
            {
                { "PRODUCT_NAME", "Cố đô Huế" },
                { "SHORT_DESCRIPTION", "Cố đô Huế, di sản văn hóa thế giới, nổi bật với kiến trúc cung đình, lăng tẩm và vẻ đẹp thơ mộng ven sông Hương." },
                { "DESCRIPTION", "Cố đô Huế, một di sản văn hóa thế giới được UNESCO công nhận, là trung tâm lịch sử và văn hóa của Việt Nam. Nơi đây nổi tiếng với quần thể kiến trúc cung đình, lăng tẩm các vị vua triều Nguyễn, cùng nhiều di tích lịch sử độc đáo. Bên dòng sông Hương thơ mộng, Huế còn hấp dẫn du khách bởi nét đẹp truyền thống, văn hóa đặc sắc và ẩm thực phong phú." },
                { "CONTENT_TYPE", "IMAGE" },
            });
            return Ok(response);
        }
    }
}
