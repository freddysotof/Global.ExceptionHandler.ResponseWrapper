using Global.ExceptionHandler.ResponseWrapper.Models;
using System.Net;

namespace Global.ExceptionHandler.ResponseWrapper.Wrappers
{
    public class SuccessResponseWrapper<T> : GenericResponseWrapper
        where T : class
    {
        public SuccessResponseWrapper()
        {
            //Data = new List<object>();
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
        public SuccessResponseWrapper(HttpStatusCode httpStatusCode, T data)
        {
            Data = data;
            StatusCode = httpStatusCode;
            IsSuccessStatusCode = true;
        }
        public SuccessResponseWrapper(HttpStatusCode httpStatusCode, T data, ResponseMessage message)
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
        public SuccessResponseWrapper(T obj)
        {
  
            //if (obj != null)
            //    Data = new List<object> { obj };

            Messages = new List<ResponseMessage>();

        }
        public SuccessResponseWrapper(IEnumerable<T> records)
        {
            //Data = records?.ToList();

            Messages = new List<ResponseMessage>();

        }
      
        public SuccessResponseWrapper(T obj = null, IEnumerable<T> records = null, ResponseMessage messageHandler = null,
            ResponseError errorResponse = null)
        {

            //if (obj != null)
            //    Data = new List<T> { obj };
            //else
            //    Data = records?.ToList();

            //if (Data == null)
            //    Data = new List<T>();

            if (messageHandler != null)
                Messages = new List<ResponseMessage> { messageHandler };
            else
                Messages = new List<ResponseMessage>();

        }

        public SuccessResponseWrapper(IEnumerable<T> records, ResponseMessage messageHandler = null)
        {
            //if (records != null)
            //    Data = records?.ToList();

            //if (Data == null)
            //    Data = new List<T>();



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
        public T Data { get; set; }
        public List<ResponseMessage> Messages { get; set; }
    }
}
