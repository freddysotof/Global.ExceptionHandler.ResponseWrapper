using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Global.ExceptionHandler.ResponseWrapper.Models
{
    public class ResponseContent
    {
        public string ContentType { get; set; }
        public long ContentLength { get; set; }
        public int StatusCode { get; set; }
        public HttpStatusCode HttpStatus { get; set; }
        public string Body { get; set; }

        public string StatusText { get => $"Status Code: {StatusCode} {HttpStatus}"; }
    }
}
