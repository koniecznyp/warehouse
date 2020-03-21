using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using Warehouse.Application.Exceptions;
using Warehouse.Core.Exceptions;

namespace Warehouse.Infrastructure.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var exceptionResponse = Map(exception);
            context.Response.StatusCode = (int)(exceptionResponse?.StatusCode ?? HttpStatusCode.BadRequest);
            var response = exceptionResponse?.Response;
            if (response is null)
            {
                await context.Response.WriteAsync(string.Empty);
                return;
            }

            var payload = JsonConvert.SerializeObject(response);
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(payload);
        }

        private static ExceptionResponse Map(Exception exception)
            => exception switch
            {
                DomainException ex => new ExceptionResponse(new { code = ex.Code, reason = ex.Message },
                    HttpStatusCode.BadRequest),
                AppException ex => new ExceptionResponse(new { code = ex.Code, reason = ex.Message },
                    HttpStatusCode.BadRequest),
                _ => new ExceptionResponse(new { code = "error", reason = "There was an error" },
                    HttpStatusCode.BadRequest)
            };
    }
}
