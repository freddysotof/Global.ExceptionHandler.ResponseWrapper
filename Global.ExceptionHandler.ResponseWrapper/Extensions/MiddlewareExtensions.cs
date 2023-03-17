using Global.ExceptionHandler.ResponseWrapper.Middleware;
using Global.ExceptionHandler.ResponseWrapper.Middlewares;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global.ExceptionHandler.ResponseWrapper.Extensions
{
    public static class MiddlewareExtensions
    {


        /// <summary>
        /// Adds the Logging middleware, which logs the incoming request's path.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseLoggingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoggingMiddleware>();
        }

     
        /// <summary>
        /// Adds the Global Error Handler Middleware.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseGlobalErrorHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalErrorHandlingMiddleware>();
        }

        /// <summary>
        /// Adds the Global Error Handler Middleware.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseGlobalErrorHandlerWithLoggingMiddleware(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<GlobalErrorHandlingMiddleware>();
            return builder.UseMiddleware<LoggingMiddleware>();
        }


        /// <summary>
        /// Adds the Pagination Response Wrapper Middleware.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UsePaginationResponseWrapperMiddleware(this IApplicationBuilder builder)
        {

            return builder.UseMiddleware<PaginationResponseWrapperMiddleware>();
        }

        /// <summary>
        /// Adds the Simple Response Wrapper Middleware.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseResponseWrapperMiddleware(this IApplicationBuilder builder)
        {

            return builder.UseMiddleware<ResponseWrapperMiddleware>();
        }

        /// <summary>
        /// Adds the Global Error Handler Middleware.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseResponseWrapperWithLoggingMiddleware(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<LoggingMiddleware>();
            return builder.UseMiddleware<ResponseWrapperMiddleware>();
        }


        /// <summary>
        /// Adds the Model State Validation Middleware.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseModelStateValidationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ModelStateValidationMiddleware>();
        }

        /// <summary>
        /// Adds the Global Error Handler and Model State Validation Middleware.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseGlobalErrorHandlerAndModelValidationMiddleware(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<GlobalErrorHandlingMiddleware>();
            return builder.UseMiddleware<ModelStateValidationMiddleware>();
        }

        /// <summary>
        /// Adds the Global Error Handler, Response Wrapper and Model State Validation Middleware.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseGlobalErrorHandlerAndResponseWrapperAndModelValidationMiddleware(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<GlobalErrorHandlingMiddleware>();
            builder.UseMiddleware<LoggingMiddleware>();
            builder.UseMiddleware<ResponseWrapperMiddleware>();
            return builder.UseMiddleware<ModelStateValidationMiddleware>();
        }
    }
}
