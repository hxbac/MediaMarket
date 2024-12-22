namespace MediaMarket.Application.DTO.Product
{
    public class CheckProductImageContentDTO
    {
        public Guid ProductId { get; set; }
        public List<string> ImagesUrl { get; set; } = new List<string>();
        public string[] PreviewsUrl { get; set; }
        public Guid ProductDetailId { get; set; }
        public string ContentType { get; } = "Image";
    }
}
