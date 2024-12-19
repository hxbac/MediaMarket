using MediaMarket.Domain.Enums;

namespace MediaMarket.Application.DTO.Request.Product
{
    public class UpdateContentStatusRequest
    {
        public Guid Id { get; set; }
        public ProductContentStatus Status { get; set; }
    }
}
