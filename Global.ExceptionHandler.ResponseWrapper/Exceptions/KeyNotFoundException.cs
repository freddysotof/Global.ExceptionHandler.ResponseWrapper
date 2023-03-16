using Global.ExceptionHandler.ResponseWrapper.Models;
using System.Net;

namespace Global.ExceptionHandler.ResponseWrapper.Exceptions
{
    public class KeyNotFoundException : CustomException
    {
        public KeyNotFoundException(string message, List<ResponseError>? errors = default)
              : base(message, errors, HttpStatusCode.NotFound) { }
    }
}
