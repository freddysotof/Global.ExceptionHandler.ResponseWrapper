using Global.ExceptionHandler.ResponseWrapper.Models;
using Global.ExceptionHandler.ResponseWrapper.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Global.ExceptionHandler.ResponseWrapper.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next,ILogger<LoggingMiddleware> loggerManager)
        {
            _next = next;
            _logger = loggerManager;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Invoking Customizations Method to handle Custom Formatted Request
                var requestContent = await FormatRequest(context.Request);

                // Save request log to chosen datastore
                _logger.LogInformation(JsonConvert.SerializeObject(requestContent));

                Stream originalBodyStream = null;
                HttpResponse response = context.Response;

                HttpStatusCode status;

                //Copy a pointer to the original response body stream
                originalBodyStream = response.Body;

                //Create a new memory stream...
                //await using var responseBody = new MemoryStream();
                var responseBody = new MemoryStream();

                //...and use that for the temporary response body
                response.Body = responseBody;

                //Continue down the Middleware pipeline, eventually returning to this class
                await _next(context);

                //We need to read the response stream from the beginning...
                responseBody.Seek(0, SeekOrigin.Begin);

                //...and copy it into a string
                var body = await new StreamReader(responseBody).ReadToEndAsync();


                // Invoking Customizations Method to handle Custom Formatted Response
                var responseContent = await FormatResponse(response, body);

                // Save response log to chosen datastore
                _logger.LogInformation(JsonConvert.SerializeObject(responseContent));

                //We need to reset the reader for the response so that the client can read it.
                responseBody.Seek(0, SeekOrigin.Begin);

                //Format the response from the server
                await responseBody.CopyToAsync(originalBodyStream);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Source);
                throw;
            }
           

        }

        private async Task<RequestContent> FormatRequest(HttpRequest request)
        {
            RequestContent requestContent = new();

            requestContent.Path = request.Path.Value;
            requestContent.Method = request.Method;
            requestContent.UserName = request.HttpContext.User.Identity.Name;
            //This line allows us to set the reader for the request back at the beginning of its stream.
            request.EnableBuffering();

            var requestReader = new StreamReader(request.Body);

            requestContent.Body = await requestReader.ReadToEndAsync();


            request.Body.Position = 0;

            requestContent.Content = $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {requestContent.Body}";
            return requestContent;
        }

        private static async Task<ResponseContent> FormatResponse(HttpResponse response,string body)
        {
            ResponseContent responseContent = new();
            
            responseContent.Body = body;

            responseContent.StatusCode = response.StatusCode;
            responseContent.HttpStatus = (HttpStatusCode)response.StatusCode;

            //Return the string for the response, including the status code (e.g. 200, 404, 401, etc.)
            return responseContent;
        }

        //public async Task InvokeAsync(HttpContext context)
        //{
        //    //Log the incoming request path
        //    _logger.Log(LogLevel.Information, context.Request.Path);

        //    //Invoke the next middleware in the pipeline
        //    await _next(context);

        //    //Get distinct response headers
        //    var uniqueResponseHeaders
        //        = context.Response.Headers
        //                          .Select(x => x.Key)
        //                          .Distinct();

        //    //Log these headers
        //    _logger.Log(LogLevel.Information, string.Join(", ", uniqueResponseHeaders));
        //}
    }
}
