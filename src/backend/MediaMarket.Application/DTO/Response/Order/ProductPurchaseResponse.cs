using MediaMarket.Domain.Enums;
using Entities = MediaMarket.Domain.Entities;

namespace MediaMarket.Application.DTO.Response.Order
{
    public class ProductPurchaseResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Slug { get; set; }
        public string? Thumbnail { get; set; }
        public long Price { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public ICollection<Entities.Category>? Categories { get; set; }
        public ICollection<Entities.Tag>? Tags { get; set; }
    }
}
