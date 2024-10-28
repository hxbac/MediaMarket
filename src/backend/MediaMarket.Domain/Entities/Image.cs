using MediaMarket.Domain.Common;

namespace MediaMarket.Domain.Entities
{
    public class Image : BaseEntity<Guid>
    {
        public Guid ProductId { get; set; }
        public string? Title { get; set; }
        public string? ImageUrl { get; set; }
        public string? FileSize { get; set; }
        public int OrderIndex { get; set; }

        public Product? Product { get; set; }
    }
}
