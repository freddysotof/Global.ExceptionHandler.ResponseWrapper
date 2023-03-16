using Global.ExceptionHandler.ResponseWrapper.Exceptions;
using Global.ExceptionHandler.ResponseWrapper.Models;
using Global.ExceptionHandler.ResponseWrapper.Wrappers;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using KeyNotFoundException = Global.ExceptionHandler.ResponseWrapper.Exceptions.KeyNotFoundException;
using NotImplementedException = Global.ExceptionHandler.ResponseWrapper.Exceptions.NotImplementedException;
using UnauthorizedAccessException = Global.ExceptionHandler.ResponseWrapper.Exceptions.UnauthorizedAccessException;

namespace Global.ExceptionHandler.ResponseWrapper.Middleware
{   /// <summary>
    /// Globar Error Handler Middleware to Manage Exceptions.
    /// </summary>
    public class GlobalErrorHandlingMiddleware
    {
        /// <summary>
        /// Request Delegate field to invoke HTTP Context
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// The Globar Error Handler Middleware Constructor
        /// </summary>
        /// <param name="next">The Request Delegate</param>
        public GlobalErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        /// <summary>
        /// Invoke Method for the HttpContext
        /// </summary>
        /// <param name="context">The HTTP Context</param>
        /// <returns>Response</returns>
        public async Task Invoke(HttpContext context)
        {
            Stream originalBodyStream = null;
            HttpResponse response = context.Response;
            HttpStatusCode status;
            try
            {
                //Copy a pointer to the original response body stream
                originalBodyStream = response.Body;

                //Continue down the Middleware pipeline, eventually returning to this class
                await _next(context);

            }
            catch (Exception ex)
            {
                //Create a new memory stream...
                await using var responseBody = new MemoryStream();
                //...and use that for the temporary response body
                response.Body = responseBody;

                //We need to read the response stream from the beginning...
                response.Body.Seek(0, SeekOrigin.Begin);

                //...and copy it into a string
                string body = await new StreamReader(response.Body).ReadToEndAsync();

                //We need to reset the reader for the response so that the client can read it.
                response.Body.Seek(0, SeekOrigin.Begin);

                try
                {
                    // Invoking Customizations Method to handle Custom Formatted Response
                    await HandleExceptionAsync(context, ex);


                    // Set the current Stream in Initial Position before copying in original stream
                    responseBody.Position = 0;


                    //Copy the contents of the new memory stream (which contains the response) to the original stream, which is then returned to the client.
                    await responseBody.CopyToAsync(originalBodyStream);
                }
                catch (Exception e)
                {

                    throw;
                }
            
            }
        }
        private async Task HandleExceptionAsync(HttpContext context,Exception ex)
        {
            var response = context.Response;


            var responseWrapper = new ErrorResponseWrapper()
            {
                Source = ex.TargetSite?.DeclaringType?.FullName,
                Exception = ex.Message.Trim(),
                SupportMessage = $"Provide the Error Id: {Guid.NewGuid()} to the support team for further analysis."
            };
            if (ex is not CustomException && ex.InnerException != null)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
            }
            ResponseError error;
            switch (ex)
            {
                case CustomException e:
                    responseWrapper.StatusCode = e.StatusCode;
                    if (e.Errors is not null)
                    {
                        responseWrapper.Errors = e.Errors;
                    }
                    break;
                default:
                    responseWrapper.StatusCode = HttpStatusCode.InternalServerError;
                    responseWrapper.Errors.Add(new("Internal Server Error from the custom middleware"));
                    break;

            };
            string formattedResponse = responseWrapper.ToString();

            // Set the Content-Type Header of the response
            response.ContentType = "application/json";

            // Set the StatusCode Header of the response
            response.StatusCode = (int)responseWrapper.StatusCode;

            //Format the response from the server
            await response.WriteAsync(formattedResponse);
        }
    }
}
