using Global.ExceptionHandler.ResponseWrapper.Exceptions;
using Global.ExceptionHandler.ResponseWrapper.Models;
using Global.ExceptionHandler.ResponseWrapper.Wrappers;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace Global.ExceptionHandler.ResponseWrapper.Middlewares
{
    /// <summary>
    /// Model State Validation Middleware to Request Delegate and handles Model Binding Exceptions.
    /// </summary>
    public class ModelStateValidationMiddleware
    {

        /// <summary>
        /// Request Delegate field to invoke HTTP Context
        /// </summary>
        private readonly RequestDelegate _next;


        /// <summary>
        /// The Model State Validation Middleware Constructor
        /// </summary>
        /// <param name="next">The Request Delegate</param>
        public ModelStateValidationMiddleware(RequestDelegate next) => _next = next;

        /// <summary>
        /// Invoke Method for the HttpContext
        /// </summary>
        /// <param name="context">The HTTP Context</param>
        /// <returns>Exception</returns>
        /// <returns>Response</returns>
        public async Task Invoke(HttpContext context)
        {

            //Continue down the Middleware pipeline, eventually returning to this class
            await _next(context);


            //First, get the incoming request
            var request = await FormatRequest(context.Request);

            // Invoking Customizations Method to handle ModelState Binding Errors
            var ModelState = context.Features.Get<ModelStateFeature>()?.ModelState;
                //  if you need pass by , just set another flag in feature .
              
                if (ModelState != null && !ModelState.IsValid)
                {
                    var errorResponse = new ErrorResponseWrapper(HttpStatusCode.BadRequest);
                    int index = 0;
                    var routeValues = context.Request.Path;
                    ModelState.Values.ToList().ForEach(modelState =>
                    {
                        var key = ModelState.Keys.ToList()[index];
                        var error = modelState.Errors.FirstOrDefault();
                        string bindingError = null;
                        if (error != null)
                        {
                            bindingError = $"{modelState.ValidationState} {key}";
                            errorResponse.Errors.Add(new(bindingError, error.ErrorMessage, modelState.AttemptedValue));
                        }
                        index++;
                    });
                // => Throw Exception to handle in Globar Error Handler Middleware
                throw new BadRequestException($"InvalidModelStateResponseFactory at {routeValues}", errorResponse.Errors);
                //throw new BadRequestException($"InvalidModelStateResponseFactory at {String.Join(", ", routeValues.Select(x => $"{x.Key}: {x.Value}"))} ", errorResponse.Errors);
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
    }
}
