using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Global.ExceptionHandler.ResponseWrapper.Models
{
    public class ResponseError:ErrorResult
    {
        public ResponseError(string statusError = null, string message = null, string stackTrace = null, string description = null)
        {
            StatusError = statusError;
            Message = message;
            StackTrace = stackTrace;
            Description = description;
        }
        public ResponseError(ErrorResult errorResult)
        {
            this.StatusError = errorResult.StatusError;
            this.Message = errorResult.Message;
            this.Description = errorResult.Description;

            this.StackTrace = errorResult.StackTrace; 
        }
    }
}
