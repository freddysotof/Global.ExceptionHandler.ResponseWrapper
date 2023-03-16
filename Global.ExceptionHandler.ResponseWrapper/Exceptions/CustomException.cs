using Global.ExceptionHandler.ResponseWrapper.Models;
using System.Net;

namespace Global.ExceptionHandler.ResponseWrapper.Exceptions
{
    public class CustomException : Exception
    {
        public List<ResponseError>? Errors { get; }

        public HttpStatusCode StatusCode { get; }

        public CustomException(string message, List<ResponseError>? errors = default, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
            : base(message)
        {
            Errors = errors;
            StatusCode = statusCode;
        }
    }
}
