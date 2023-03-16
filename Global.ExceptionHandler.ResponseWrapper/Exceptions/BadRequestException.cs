using Global.ExceptionHandler.ResponseWrapper.Models;
using System.Net;

namespace Global.ExceptionHandler.ResponseWrapper.Exceptions
{
    public class BadRequestException :CustomException
    {
        public BadRequestException(string message, List<ResponseError>? errors = default)
            : base(message, errors, HttpStatusCode.BadRequest) { }
    }
}
