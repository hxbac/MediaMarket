using MediaMarket.Application.DTO.Request.Common;
using MediaMarket.Domain.Enums;

namespace MediaMarket.Application.DTO.Request.Product
{
    public class SearchProductWithTypeRequest : SearchRequest
    {
        public ProductType Type { get; set; }
    }
}
