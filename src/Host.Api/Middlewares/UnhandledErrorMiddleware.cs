using System;
using System.Net;
using System.Threading.Tasks;
using Domain.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Host.Api.Middlewares
{
    public class UnhandledErrorMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<UnhandledErrorMiddleware> _logger;

        public UnhandledErrorMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = loggerFactory?.CreateLogger<UnhandledErrorMiddleware>() ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (DomainValidationException ex)
            {
                await HandleValidationExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleValidationExceptionAsync(HttpContext context, DomainValidationException ex)
        {
            await DeliverExceptionAsync(
                context,
                HttpStatusCode.BadRequest,
                JsonConvert.SerializeObject(new
                {
                    ex.Message,
                    ex.Errors
                }));
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            await DeliverExceptionAsync(
                context, 
                HttpStatusCode.InternalServerError,
                "We had a problem. We have a cool logging system, so no worries we'll be looking at it soon.");
        }

        private static async Task DeliverExceptionAsync(HttpContext context, HttpStatusCode statusCode, string message)
        {
            context.Response.Clear();
            context.Response.StatusCode = (int)statusCode;
            //context.Response.ContentType = ex.ContentType;
            await context.Response.WriteAsync(message);
        }
    }

    public static class UnhandledErrorMiddlewareExtension
    {
        public static IApplicationBuilder UseUnhandledErrorMiddlewareExtension(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UnhandledErrorMiddleware>();
        }
    }
}
