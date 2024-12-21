using MediaMarket.Application.DTO.Request.Common;
using MediaMarket.Domain.Enums;

namespace MediaMarket.Application.DTO.Request.Product
{
    public class GetProductListRequest : PaginationRequest
    {
        public ProductType ProductType { get; set; } = ProductType.None;
        public string? Name { get; set; }
    }
}
