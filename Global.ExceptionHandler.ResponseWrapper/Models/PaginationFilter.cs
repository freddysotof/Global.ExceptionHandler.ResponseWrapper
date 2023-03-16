using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global.ExceptionHandler.ResponseWrapper.Models
{
    public class PaginationFilter
    {
        public int? Limit { get; set; }
        public int Offset { get; set; }
        public int PageSize { get => (int)Limit; }
        public int PageNumber { get => (int)Math.Ceiling(Offset == 0 ? 1 : (decimal)(Offset + (int)Limit) / (int)Limit); }
        //public int PageNumber { get => Math.Ceiling(Offset==0 ? 1 : ((Offset + Limit) / Limit))); }
        public bool Pageable { get => Limit.HasValue; }
        public PaginationFilter()
        {
            Limit = 10;
            Offset = 0;
        }
        public PaginationFilter(int? limit, int? offset = 0)
        {
            Limit = limit;
            Offset = offset ?? 0;
            //int? pageLength = limit;
            //// Si no tiene valor el offset entonces tomara por defecto la pagina 1.
            //int? pageNumber = !offset.HasValue ? 1 : (((int)offset + limit) / limit);
        }
    }
}
