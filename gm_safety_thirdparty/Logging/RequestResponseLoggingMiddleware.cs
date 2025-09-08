using System.Text;

namespace Logging
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestResponseLoggingMiddleware> _logger;

        public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            _logger.LogInformation("➡️ {Method} {Path}", context.Request.Method, context.Request.Path);

            // Optionally log request body (small payloads)
            if (context.Request.ContentLength is > 0)
            {
                context.Request.EnableBuffering();
                context.Request.Body.Position = 0;
                using var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true);
                var body = await reader.ReadToEndAsync();
                context.Request.Body.Position = 0;
                if (!string.IsNullOrWhiteSpace(body))
                    _logger.LogInformation("Request Body: {Body}", body);
            }

            var originalBodyStream = context.Response.Body;
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            await _next(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            _logger.LogInformation("⬅️ Status: {StatusCode}", context.Response.StatusCode);
            if (!string.IsNullOrWhiteSpace(text))
                _logger.LogInformation("Response Body: {Body}", text);

            await responseBody.CopyToAsync(originalBodyStream);
        }
    }
}
