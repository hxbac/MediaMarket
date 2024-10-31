using MediaMarket.Domain.Enums;

namespace MediaMarket.Application.DTO.Response.Product
{
    public class ProductHomePageResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Thumbnail { get; set; }
        public string? ShortDescription { get; set; }
        public required long Price { get; set; }
        public ProductType ProductType { get; set; }
    }
}
