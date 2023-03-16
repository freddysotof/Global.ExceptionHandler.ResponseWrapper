using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Global.ExceptionHandler.ResponseWrapper.Models
{
    public class ErrorResult
    {
        public ErrorResult(string statusError = null, string message = null, string stackTrace = null, string description = null)
        {
            StatusError = statusError;
            Message = message;
            StackTrace = stackTrace;
            Description = description;
        }
        public string StatusError { get; set; }
        public string Message { get; set; }
        public string Description { get; set; }
        public string StackTrace { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
        //public override string ToString()
        //    => $"Error: ({nameof(StatusError)}: {StatusError}; {nameof(Message)}: {Message}; {nameof(StackTrace)}: {StackTrace})";


    }
}
