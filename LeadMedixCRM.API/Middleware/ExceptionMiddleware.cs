using LeadMedixCRM.API.Common;
using LeadMedixCRM.Application.Exceptions;
using System;
using System.Net;
using System.Text.Json;

namespace LeadMedixCRM.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "An unhandled exception occurred.");
                _logger.LogError(ex,
                    "Unhandled exception. Path: {Path}, Method: {Method}",
                    context.Request.Path,
                    context.Request.Method);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode statusCode;
            string message = exception.Message;

            switch (exception)
            {
                case NotFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    break;

                case ValidationException:
                    statusCode = HttpStatusCode.BadRequest;
                    break;

                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    message = "An unexpected error occurred.";
                    break;
            }

            //var response = new
            //{
            //    success = false,
            //    message,
            //};
            var response = ApiResponse<string>.FailureResponse(message);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
