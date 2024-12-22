namespace MediaMarket.Application.DTO.Request.Product
{
    public class CreateProductPreviewRequest
    {
        public Guid ProductId { get; set; }
        public List<string> PreviewUrls { get; set; }
    }
}
