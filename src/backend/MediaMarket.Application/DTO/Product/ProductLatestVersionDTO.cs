namespace MediaMarket.Application.DTO.Product
{
    public class ProductLatestVersionDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public long Price { get; set; }
        public int Version { get; set; }
    }
}
