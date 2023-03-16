
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Global.ExceptionHandler.ResponseWrapper.Wrappers
//{
//    public class PagedListWrapper<T>
//    {
//        public int FirstItemOnPage { get; set; }
//        public bool HasNextPage { get; set; }
//        public bool HasPreviousPage { get; set; }
//        public bool IsFirstPage { get; set; }
//        public bool IsLastPage { get; set; }
//        public int LastItemOnPage { get; set; }
//        public int PageCount { get; set; }
//        public int PageNumber { get; set; }
//        public int PageSize { get; set; }
//        public int TotalItemCount { get; set; }
//        public IEnumerable<T> Subset { get; set; }
//    }
//    public class Converter<TSource, TDestination> : ITypeConverter<IPagedList<TSource>, PagedListWrapper<TDestination>>
//    {
//        public PagedListWrapper<TDestination> Convert(IPagedList<TSource> source, PagedListWrapper<TDestination> destination, ResolutionContext context)
//        {
//            return new PagedListWrapper<TDestination>()
//            {
//                FirstItemOnPage = source.FirstItemOnPage,
//                HasNextPage = source.HasNextPage,
//                HasPreviousPage = source.HasPreviousPage,
//                IsFirstPage = source.IsFirstPage,
//                IsLastPage = source.IsLastPage,
//                LastItemOnPage = source.LastItemOnPage,
//                PageCount = source.PageCount,
//                PageNumber = source.PageNumber,
//                PageSize = source.PageSize,
//                TotalItemCount = source.TotalItemCount,
//                Subset = context.Mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(source) //User mapper to go from "foo" to "bar"
//            };
//        }
//    }
//}
