﻿using Global.ExceptionHandler.ResponseWrapper.Services;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace Global.ExceptionHandler.ResponseWrapper.Middleware
{
    /// <summary>
    /// Response Wrapper Middleware to Request Delegate and handles Response Customizations.
    /// </summary>
    public class ResponseWrapperMiddleware
    {
        /// <summary>
        /// Request Delegate field to invoke HTTP Context
        /// </summary>
        private readonly RequestDelegate _next;
        private IResponseWrapperService _wrapperService;
        /// <summary>
        /// The Response Wrapper Middleware Constructor
        /// </summary>
        /// <param name="next">The Request Delegate</param>
        public ResponseWrapperMiddleware(RequestDelegate next) => _next = next;

        /// <summary>
        /// Invoke Method for the HttpContext
        /// </summary>
        /// <param name="context">The HTTP Context</param>
        /// <param name="wrapperService">The Wrapper Service to return custom response (paginated response or simple response)</param>
        /// <returns>Response</returns>
        public async Task Invoke(HttpContext context,IResponseWrapperService wrapperService)
        {
            _wrapperService = wrapperService;


            Stream originalBodyStream = null;
            HttpResponse response = context.Response;
            HttpStatusCode status;

            //Copy a pointer to the original response body stream
            originalBodyStream = response.Body;

            //Create a new memory stream...
            await using var responseBody = new MemoryStream();

            //...and use that for the temporary response body
            response.Body = responseBody;

            //Continue down the Middleware pipeline, eventually returning to this class
            await _next(context);

            //We need to read the response stream from the beginning...
            response.Body.Seek(0, SeekOrigin.Begin);

            //...and copy it into a string
            string body = await new StreamReader(response.Body).ReadToEndAsync();

            //We need to reset the reader for the response so that the client can read it.
            response.Body.Seek(0, SeekOrigin.Begin);


            // Verify if response body has any value
            if (body.Length > 0)
            {
                // Invoking Customizations Method to handle Custom Formatted Response
                var responseHandler = _wrapperService.WrapPagedResponse(body, context);


                string formattedBody = responseHandler.ToString();

                // Set the Content-Type Header of the response
                response.ContentType = "application/json";

                // Set the current Stream Content Length before copying in original stream
                response.ContentLength = formattedBody.Length;
                
                // Set the StatusCode Header of the response
                response.StatusCode = (int)responseHandler.StatusCode;

                //Format the response from the server
                await response.WriteAsync(formattedBody);

                // Set the current Stream in Initial Position before copying in original stream
                responseBody.Position = 0;

                // Set the current Stream Content Length before copying in original stream
                responseBody.SetLength(formattedBody.Length);

                //Format the response from the server
                await responseBody.CopyToAsync(originalBodyStream);
            }

        }

        //private async Task HandleResponseAsync(HttpContext context)
        //{
          
        //}
    }
}

