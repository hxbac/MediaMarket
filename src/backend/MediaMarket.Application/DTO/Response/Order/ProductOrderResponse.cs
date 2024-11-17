using Entities = MediaMarket.Domain.Entities;

namespace MediaMarket.Application.DTO.Response.Order
{
    public class ProductOrderResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Slug { get; set; }
        public string? Thumbnail { get; set; }
        public long Price { get; set; }
        public ICollection<Entities.Category>? Categories { get; set; }
    }
}
