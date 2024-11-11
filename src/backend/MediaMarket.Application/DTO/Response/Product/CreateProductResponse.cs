using MediaMarket.Domain.Enums;

namespace MediaMarket.Application.DTO.Response.Product
{
    public class CreateProductResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Thumbnail { get; set; }
        public string? ShortDescription { get; set; }
        public string? Description { get; set; }
        public long Price { get; set; }
        public Guid SellerId { get; set; }
        public ProductType ProductType { get; set; }
        public ProductStatus ProductStatus { get; set; }
        public ProductContentStatus ProductContentStatus { get; set; }
    }
}
