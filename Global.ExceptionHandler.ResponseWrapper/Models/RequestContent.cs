using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global.ExceptionHandler.ResponseWrapper.Models
{
    public class RequestContent
    {
        public string UserName { get; set; }
        public string Path { get; set; }
        public string Method { get; set; }
        public string Body { get; set; }
        public string Content { get; set; }
    }
}
