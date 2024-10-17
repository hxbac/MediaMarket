using System.Net;

namespace MediaMarket.Application.Bases
{
    public class BaseResponseHandler
    {
        public BaseResponse<T> Created<T>(T entity, object meta = null)
        {
            return new BaseResponse<T>()
            {
                Data = entity,
                StatusCode = HttpStatusCode.Created,
                Succeeded = true,
                Message = "Create successfully",
                Meta = meta
            };
        }

        public BaseResponse<T> Deleted<T>()
        {
            return new BaseResponse<T>()
            {
                StatusCode = HttpStatusCode.OK,
                Succeeded = true,
                Message = "Delete successfully"
            };
        }

        public BaseResponse<T> Success<T>(T entity, object meta = null)
        {
            return new BaseResponse<T>()
            {
                Data = entity,
                StatusCode = HttpStatusCode.OK,
                Succeeded = true,
                Message = "Successfully",
                Meta = meta
            };
        }

        public BaseResponse<T> Unauthorized<T>()
        {
            return new BaseResponse<T>()
            {
                StatusCode = HttpStatusCode.Unauthorized,
                Succeeded = true,
                Message = "Unauthorized"
            };
        }

        public BaseResponse<T> BadRequest<T>(string message, List<string> errors = null)
        {
            return new BaseResponse<T>()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Succeeded = false,
                Message = string.IsNullOrWhiteSpace(message) ? "Bad request" : message,
                Errors = errors
            };
        }

        public BaseResponse<T> NotFound<T>(string message = null)
        {
            return new BaseResponse<T>()
            {
                StatusCode = HttpStatusCode.NotFound,
                Succeeded = false,
                Message = string.IsNullOrWhiteSpace(message) ? "Not found" : message
            };
        }
    }
}
