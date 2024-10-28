using MediaMarket.Domain.Common;

namespace MediaMarket.Domain.Entities
{
    public class ProductDetail : BaseEntity<Guid>
    {
        public Guid ProductId { get; set; }
        public string? FileUrl { get; set; }
        public string? FileInfo { get; set; }
        public int Version { get; set; }

        public Product? Product { get; set; }
    }
}
