namespace MediaMarket.Application.DTO.Product
{
    public class CheckProductImageContentDTO
    {
        public Guid Id { get; set; }
        public List<string> ImagesUrl { get; set; } = new List<string>();
    }
}
