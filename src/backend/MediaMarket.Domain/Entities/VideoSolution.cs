using MediaMarket.Domain.Common;

namespace MediaMarket.Domain.Entities
{
    public class VideoSolution : BaseEntity<Guid>
    {
        public Guid ProductId { get; set; }
        public string? Resolution { get; set; }
        public string? FileUrl { get; set; }
        public string? FileSize { get; set; }

        public Product? Product { get; set; }
    }
}
