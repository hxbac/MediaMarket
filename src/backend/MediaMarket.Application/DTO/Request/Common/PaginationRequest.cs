namespace MediaMarket.Application.DTO.Request.Common
{
    public class PaginationRequest
    {
        public int Page { get; set; }
        public int PageSize { get; set; } = 10;
    }
}
