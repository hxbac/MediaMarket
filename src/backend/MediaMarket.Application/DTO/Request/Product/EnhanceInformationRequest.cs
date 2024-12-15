using MediaMarket.Domain.Enums;

namespace MediaMarket.Application.DTO.Request.Product
{
    public class EnhanceInformationRequest
    {
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public ProductType ProductType { get; set; }
    }
}
