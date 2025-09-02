using Business.ServicesLayer.Exceptions;
using System.Text.Json;

namespace Business.ServicesLayer.Middelwares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger, IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
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
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            int statusCode;
            string message = exception.Message;

            if (exception is ValidationException)
                statusCode = StatusCodes.Status400BadRequest;
            else if (exception is NotFoundException)
                statusCode = StatusCodes.Status404NotFound;
            else if (exception is UnauthorizedException)
                statusCode = StatusCodes.Status401Unauthorized;
            else if (exception is ForbiddenException)
                statusCode = StatusCodes.Status403Forbidden;
            else if (exception is ConflictException)
                statusCode = StatusCodes.Status409Conflict;
            else
                statusCode = StatusCodes.Status500InternalServerError;

            var ApiErrorResponse = new
            {
                statusCode,
                message,
                path = context.Request.Path,
                timestamp = DateTime.UtcNow
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(JsonSerializer.Serialize(ApiErrorResponse));
        }

    }

}

