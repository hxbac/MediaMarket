using System.Net;

namespace MediaMarket.Application.Bases
{
    public class PaginatedResponse<T>
    {
        public PaginatedResponse(List<T> data)
        {
            Data = data;
        }
        public List<T> Data { get; set; }

        internal PaginatedResponse(bool succeeded, List<T> data = default, List<string> messages = null, int count = 0, int page = 1, int pageSize = 10, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            Data = data;
            CurrentPage = page;
            Succeeded = succeeded;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
            StatusCode = statusCode;
        }

        public static PaginatedResponse<T> Success(List<T> data, int count, int page, int pageSize)
        {
            return new(true, data, null, count, page, pageSize, System.Net.HttpStatusCode.OK);
        }
        public static PaginatedResponse<T> InternalServerError(List<T> data, int count, int page, int pageSize)
        {
            return new(false, new List<T>(), null, count, page, pageSize, System.Net.HttpStatusCode.InternalServerError);
        }
        public static PaginatedResponse<T> Unauthorized(List<T> data, int count, int page, int pageSize)
        {
            return new(false, new List<T>(), null, count, page, pageSize, System.Net.HttpStatusCode.Unauthorized);
        }
        public static PaginatedResponse<T> BadRequest(List<T> data, int count, int page, int pageSize)
        {
            return new(false, new List<T>(), null, count, page, pageSize, System.Net.HttpStatusCode.BadRequest);
        }
        public HttpStatusCode StatusCode { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int TotalCount { get; set; }

        public object Meta { get; set; }

        public int PageSize { get; set; }

        public bool HasPreviousPage => CurrentPage > 1;

        public bool HasNextPage => CurrentPage < TotalPages;

        public List<string> Messages { get; set; } = new();

        public bool Succeeded { get; set; }
    }
}
