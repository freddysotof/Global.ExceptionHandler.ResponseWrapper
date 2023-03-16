using Global.ExceptionHandler.ResponseWrapper.Models;
using System.Net;

namespace Global.ExceptionHandler.ResponseWrapper.Exceptions
{
    public class UnauthorizedAccessException : CustomException
    {
        public UnauthorizedAccessException(string message, List<ResponseError>? errors = default)
              : base(message, errors, HttpStatusCode.Unauthorized) { }
    }
}
