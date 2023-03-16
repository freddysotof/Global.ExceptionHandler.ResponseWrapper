using System.Net;

namespace Global.ExceptionHandler.ResponseWrapper.Exceptions
{
    public class NotFoundException : CustomException
    {
        public NotFoundException(string message)
            : base(message, null, HttpStatusCode.NotFound)
        {
        }
    }
}
