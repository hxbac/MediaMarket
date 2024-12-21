namespace MediaMarket.Application.DTO.Product
{
    public class HandleProductVideoContentDTO
    {
        public Guid ProductId { get; set; }
        public string FileUrl { get; set; }
        public string[] RangePreview { get; set; }
        public Guid ProductDetailId { get; set; }
        public string ProductType = "Video";
    }
}
