using System.Net;
using System.Text;
using System.Text.Json;

namespace Ecommerce.Api.Middlewares
{
    public class ErrorLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _logFilePath;
        private readonly IWebHostEnvironment _env;

        public ErrorLoggingMiddleware(RequestDelegate next, IConfiguration configuration, IWebHostEnvironment env)
        {
            _next = next;
            _logFilePath = configuration["Logging:ErrorLogFilePath"] ?? "Logs/errors.log";
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await LogErrorToFileAsync(ex);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var errorResponse = new
                {
                    StatusCode = context.Response.StatusCode,
                    Message = "An unexpected error occurred.",
                    Details = _env.IsDevelopment() ? ex.Message : null 
                };

                var json = JsonSerializer.Serialize(errorResponse);
                await context.Response.WriteAsync(json);
            }
        }

        private async Task LogErrorToFileAsync(Exception exception)
        {
            var logEntry = new StringBuilder();
            logEntry.AppendLine("----- ERROR -----");
            logEntry.AppendLine($"Timestamp: {DateTime.Now}");
            logEntry.AppendLine($"Message: {exception.Message}");
            logEntry.AppendLine($"StackTrace: {exception.StackTrace}");
            logEntry.AppendLine();

            var logDir = Path.GetDirectoryName(_logFilePath);
            if (!Directory.Exists(logDir))
            {
                Directory.CreateDirectory(logDir!);
            }

            await File.AppendAllTextAsync(_logFilePath, logEntry.ToString());
        }
    }
}
