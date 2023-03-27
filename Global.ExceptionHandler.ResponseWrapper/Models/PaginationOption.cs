using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global.ExceptionHandler.ResponseWrapper.Models
{
    public class PaginationOption
    {
        public int? Limit { get; set; }
        public int Offset { get; set; }
    }
}
