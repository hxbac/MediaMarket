using MediaMarket.Domain.Common;
using MediaMarket.Domain.Enums;

namespace MediaMarket.Domain.Entities
{
    public class Product : BaseAuditableEntity<Guid>
    {
        public string? Name { get; set; }
        public string? Slug { get; set; }
        public string? Thumbnail { get; set; }
        public string? ShortDescription { get; set; }
        public string? Description { get; set; }
        public long Price { get; set; }
        public ProductType ProductType { get; set; }
        public ProductStatus ProductStatus { get; set; }
        public ProductContentStatus ProductContentStatus { get; set; }

        public User? Seller { get; }
        public ICollection<Tag>? Tags { get; set; }
        public ICollection<Category>? Categories { get; set; }
        public ICollection<ProductDetail>? ProductDetails { get; set; }
        public ICollection<Image>? Images { get; set; }
        public Preview? Preview { get; set; }
        public ICollection<VideoSolution>? VideoSolutions { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}
