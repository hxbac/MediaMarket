using MediaMarket.Domain.Common;

namespace MediaMarket.Domain.Entities
{
    public class Preview : BaseEntity<Guid>
    {
        public Guid ProductId { get; set; }
        public bool WatermarkEnabled { get; set; }
        public string? PreviewInfo { get; set; }

        public Product? Product { get; set; }
    }
}
