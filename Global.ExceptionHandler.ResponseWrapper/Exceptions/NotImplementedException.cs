using Global.ExceptionHandler.ResponseWrapper.Models;
using System.Net;

namespace Global.ExceptionHandler.ResponseWrapper.Exceptions
{
    public class NotImplementedException : CustomException
    {
        public NotImplementedException(string message, List<ResponseError>? errors = default)
              : base(message, errors, HttpStatusCode.NotImplemented) { }
    }
}

