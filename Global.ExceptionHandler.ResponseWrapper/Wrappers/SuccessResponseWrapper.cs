using Global.ExceptionHandler.ResponseWrapper.Models;
using System.Net;

namespace Global.ExceptionHandler.ResponseWrapper.Wrappers
{
    public class SuccessResponseWrapper : GenericResponseWrapper
    {
        public SuccessResponseWrapper()
        {
            Data = new List<object>();
            Messages = new List<ResponseMessage>();
        }
        public SuccessResponseWrapper(HttpStatusCode httpStatusCode, ResponseMessage message)
        {
            Messages = new()
            {
                message
            };
            StatusCode = httpStatusCode;
            IsSuccessStatusCode = true;
        }
        public SuccessResponseWrapper(HttpStatusCode httpStatusCode, object data)
        {
            Data = data;
            StatusCode = httpStatusCode;
            IsSuccessStatusCode = true;
        }
        public SuccessResponseWrapper(HttpStatusCode httpStatusCode, object data, ResponseMessage message)
        {
            Data = data;
            StatusCode = httpStatusCode;
            IsSuccessStatusCode = true;
            Messages = new()
            {
                message
            };
        }
        public SuccessResponseWrapper(HttpStatusCode httpStatusCode)
        {
            Messages = new();
            StatusCode = httpStatusCode;
            IsSuccessStatusCode = true;
        }
        public SuccessResponseWrapper(object obj)
        {
            //AppCode = appCode;

            if (obj != null)
                Data = new List<object> { obj };

            Messages = new List<ResponseMessage>();

        }
        public SuccessResponseWrapper(IEnumerable<object> records)
        {
            Data = records?.ToList();

            Messages = new List<ResponseMessage>();

        }
      
        public SuccessResponseWrapper(object obj = null, IEnumerable<object> records = null, ResponseMessage messageHandler = null,
            ResponseError errorResponse = null)
        {
            //AppCode = appCode;

            if (obj != null)
                Data = new List<object> { obj };
            else
                Data = records?.ToList();

            if (Data == null)
                Data = new List<object>();

            if (messageHandler != null)
                Messages = new List<ResponseMessage> { messageHandler };
            else
                Messages = new List<ResponseMessage>();

        }

        public SuccessResponseWrapper(IEnumerable<object> records, ResponseMessage messageHandler = null)
        {
            if (records != null)
                Data = records?.ToList();

            if (Data == null)
                Data = new List<object>();



            if (messageHandler != null)
                Messages = new List<ResponseMessage> { messageHandler };
            else
                Messages = new List<ResponseMessage>();

        }
        public SuccessResponseWrapper(ResponseMessage messageHandler = null)
        {
            if (messageHandler != null)
                Messages = new List<ResponseMessage> { messageHandler };
            else
                Messages = new List<ResponseMessage>();
        }
        public SuccessResponseWrapper(List<ResponseMessage> messageHandler = null)
        {
            Messages = messageHandler;
        }
        public object Data { get; set; }
        public List<ResponseMessage> Messages { get; set; }
    }
}
