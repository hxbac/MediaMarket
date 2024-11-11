using MediaMarket.Domain.Enums;

namespace MediaMarket.Application.DTO.Request.Product
{
    public class CreateProductRequest
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string ShortDescription { get; set; }
        public required string Thumbnail { get; set; }
        public long Price { get; set; }
        public required List<Guid> CategoryIds { get; set; }
        public required List<string> Tags { get; set; }
        public required List<FileInfo> OriginalFiles { get; set; }
        public ProductType Type { get; set; }
        public ProductStatus Status { get; set; }
    }

    public class FileInfo
    {
        public Guid? Id { get; set; }
        public required string Url { get; set; }
    }
}
