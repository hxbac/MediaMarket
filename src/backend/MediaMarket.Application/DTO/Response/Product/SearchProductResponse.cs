namespace MediaMarket.Application.DTO.Response.Product
{
    public class SearchProductResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? ShortDescription { get; set; }
        public string? Thumbnail { get; set; }
        public long Price { get; set; }
    }
}
