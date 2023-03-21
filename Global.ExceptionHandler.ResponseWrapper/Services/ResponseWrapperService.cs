
using Global.ExceptionHandler.ResponseWrapper.Extensions;
using Global.ExceptionHandler.ResponseWrapper.Models;
using Global.ExceptionHandler.ResponseWrapper.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Newtonsoft.Json;
using System.Net;

namespace Global.ExceptionHandler.ResponseWrapper.Services
{

    public class ResponseWrapperService<T> : IResponseWrapperService<T>
        where T : class
    {
        private readonly IPageUriService _pageUriService;
        private readonly IHttpContextAccessor _contextAccessor;
        public ResponseWrapperService(IPageUriService pageUriService,IHttpContextAccessor httpContextAccessor
            )
        {
            _pageUriService = pageUriService ?? throw new ArgumentNullException(nameof(pageUriService));
            _contextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public SuccessResponseWrapper<T> WrapPagedResponse(string responseBody)
        {
            var response = _contextAccessor.HttpContext.Response;
            var request = _contextAccessor.HttpContext.Request;

            var requestUrl = request.GetDisplayUrl();
            var status = responseBody != null;
            var httpStatusCode = (HttpStatusCode)response.StatusCode;
            if (!responseBody.TryParseJson<List<T>>(out List<T> data))
            {
                return new SuccessResponseWrapper<T>(httpStatusCode, JsonConvert.DeserializeObject<T>(responseBody));
            }


            // NOTE: Add any further customizations if needed here
            int? limit = null;
            int offset = 0;
            string sort = null;
            string route = request.Path.Value;
            var queryParams = request.Query;
            if (queryParams.ContainsKey("limit"))
                limit = int.Parse(queryParams["limit"]);

            if (queryParams.ContainsKey("offset"))
                offset = int.Parse(queryParams["offset"]);

            if (queryParams.ContainsKey("sort"))
                sort = queryParams["sort"];

            var validFilter = new PaginationFilter(limit, offset);
            if (!validFilter.Pageable || validFilter.PageSize == 0)
                return new SuccessResponseWrapper<T>(httpStatusCode, JsonConvert.DeserializeObject<T>(responseBody));
            else
            {
                var totalRecords = data.Count();
                if (totalRecords == 0)
                {
                    var row = JsonConvert.SerializeObject(data);
                    return new SuccessResponseWrapper<T>(httpStatusCode, JsonConvert.DeserializeObject<T>(row));
                }
                  
                data = data
              .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize)
              .ToList();


                var pagedRow = JsonConvert.SerializeObject(data);
                var pagedResponse = new PagedResponseWrapper<T>(JsonConvert.DeserializeObject<T>(pagedRow), validFilter.PageNumber, validFilter.PageSize);

                var totalPages = ((double)totalRecords / (double)validFilter.PageSize);
                var lastOffset = ((int)totalRecords - validFilter.Limit);
                int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
                pagedResponse.NextPage =
                    validFilter.PageNumber >= 1 && validFilter.PageNumber < roundedTotalPages
                    ? _pageUriService.GetPageUri(new PaginationFilter(validFilter.Limit, validFilter.Offset + validFilter.Limit), route)
                    : null;
                pagedResponse.PreviousPage =
                    validFilter.PageNumber - 1 >= 1 && validFilter.PageNumber <= roundedTotalPages
                    ? _pageUriService.GetPageUri(new PaginationFilter(validFilter.Limit, validFilter.Offset - validFilter.Limit), route)
                    : null;
                pagedResponse.FirstPage = _pageUriService.GetPageUri(new PaginationFilter(validFilter.Limit, 0), route);
                pagedResponse.LastPage = _pageUriService.GetPageUri(new PaginationFilter(validFilter.Limit, lastOffset), route);
                pagedResponse.TotalPages = roundedTotalPages;
                pagedResponse.TotalRecords = totalRecords;
                return pagedResponse;

            }

        }

        public SuccessResponseWrapper<T> WrapResponse(string responseBody)
        {
            var response = _contextAccessor.HttpContext.Response;
            var status = responseBody != null;
            var httpStatusCode = (HttpStatusCode)response.StatusCode;
            return new SuccessResponseWrapper<T>(httpStatusCode, JsonConvert.DeserializeObject<T>(responseBody));
        }

    }
}
