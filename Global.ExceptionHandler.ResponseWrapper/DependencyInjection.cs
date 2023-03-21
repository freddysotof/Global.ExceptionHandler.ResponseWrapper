using Global.ExceptionHandler.ResponseWrapper.Filters;
using Global.ExceptionHandler.ResponseWrapper.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Global.ExceptionHandler.ResponseWrapper
{
    public static class DependencyInjection
    {
        public static void AddResponseWrapper(this IServiceCollection services)
        {
            services.AddSingleton<IPageUriService>(o =>
            {
                var accessor = o.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext?.Request;
                string uri = string.Empty;
                if (request != null)
                    uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new PageUriService(uri);
            });

            services.AddSingleton(typeof(IResponseWrapperService<>), typeof(ResponseWrapperService<>));

        }

        public static void ConfigureCustomModelStateValidation(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(opts =>
            {
                opts.InvalidModelStateResponseFactory = ctx => new ModelStateFeatureAction();
            });
        }
    }
}
