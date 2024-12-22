namespace MediaMarket.Application.DTO.Request.Product
{
    public class UpdateProductDetailRequest
    {
        public Guid ProductDetailId { get; set; }
        public string FileUrl { get; set; }
    }
}
