using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SalesOrganizer
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                try
                {
                    _logger.LogError($"Something went wrong: {ex}");
                }
                catch (Exception iex)
                {
                    Console.WriteLine($"Exception in Logger caught by Global Exception Middleware {JsonConvert.SerializeObject(iex)}");
                }
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(JsonConvert.SerializeObject(new GlobalExceptionDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = "Internal Server Error from Global Exception Middleware"
            }));
        }
    }
}
