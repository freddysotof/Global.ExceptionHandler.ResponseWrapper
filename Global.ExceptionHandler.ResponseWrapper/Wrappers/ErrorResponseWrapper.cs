using System.Net;
using Global.ExceptionHandler.ResponseWrapper.Models;

namespace Global.ExceptionHandler.ResponseWrapper.Wrappers
{
    public class ErrorResponseWrapper : GenericResponseWrapper
    {
        public ErrorResponseWrapper()
        {
            Errors = new();
        }
        public ErrorResponseWrapper(HttpStatusCode httpStatusCode, ResponseError error)
        {
            Errors = new()
            {
                error
            };
            StatusCode = httpStatusCode;
            IsSuccessStatusCode = false;
        }
        public ErrorResponseWrapper(HttpStatusCode httpStatusCode)
        {
            Errors = new();
            StatusCode = httpStatusCode;
            IsSuccessStatusCode = false;
        }
        public List<ResponseError> Errors { get; set; }
        public string Exception { get; set; }
        public string Source { get; set; }
        public string SupportMessage { get; set; }

    }
}
