using MediaMarket.Domain.Common;
using MediaMarket.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace MediaMarket.Domain.Entities
{
    public class Product : BaseAuditableEntity<Guid>
    {
        [MaxLength(150)]
        public required string Name { get; set; }
        public string? Thumbnail { get; set; }
        public string? ShortDescription { get; set; }
        public string? Description { get; set; }
        public required long Price { get; set; }
        public required Guid SellerId { get; set; }
        public User? Seller { get; }
        public ProductType ProductType { get; set; }
        public ProductStatus ProductStatus { get; set; }
        public ProductContentStatus ProductContentStatus { get; set; }
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
        public ICollection<Category> Categories { get; set; } = new List<Category>();
    }
}
