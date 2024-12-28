using MediaMarket.Domain.Entities;

namespace MediaMarket.Application.DTO.Response.Product
{
    public class ProductCheckoutResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Slug { get; set; }
        public long Price { get; set; }
        public string? Thumbnail { get; set; }
        public string? ShortDescription { get; set; }

        public ICollection<ProductDiscount> ProductDiscounts { get; set; } = new List<ProductDiscount>();
        public List<Domain.Entities.EventDiscount> EventDiscounts { get; set; } = new List<Domain.Entities.EventDiscount>();
    }
}
