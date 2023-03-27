using Global.ExceptionHandler.ResponseWrapper.Models;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global.ExceptionHandler.ResponseWrapper.Services
{
    public interface IPageUriService
    {
        public Uri GetPageUri(Pagination filter, string route);
    }
    public class PageUriService : IPageUriService
    {
        private readonly string _baseUri;
        public PageUriService(string baseUri)
        {
            _baseUri = baseUri;
        }
        public Uri GetPageUri(Pagination filter, string route)
        {
            var _enpointUri = new Uri(string.Concat(_baseUri, route));
            var modifiedUri = QueryHelpers.AddQueryString(_enpointUri.ToString(), "limit", filter.Limit?.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "offset", filter.Offset.ToString());
            return new Uri(modifiedUri);
        }
    }
}
