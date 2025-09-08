using System.Net.Http.Headers;
using System.Text;
using gm_safety_thirdparty.Auth;
using Microsoft.Extensions.Options;

namespace gm_safety_thirdparty.Middleware;

public class AuthMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<AuthMiddleware> _logger;

    public AuthMiddleware(RequestDelegate next, ILogger<AuthMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context, IOptions<AuthOptions> options)
    {
        var cfg = options.Value;
        var headers = context.Request.Headers;

        // 1) Basic Auth
        if (!headers.TryGetValue("Authorization", out var authHeaderValues))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Missing Authorization header.");
            return;
        }

        var authHeader = AuthenticationHeaderValue.Parse(authHeaderValues!);
        if (!"Basic".Equals(authHeader.Scheme, StringComparison.OrdinalIgnoreCase))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Invalid auth scheme.");
            return;
        }

        var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authHeader.Parameter ?? ""));
        var parts = credentials.Split(':', 2);
        if (parts.Length != 2)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Invalid basic auth header.");
            return;
        }

        var (username, password) = (parts[0], parts[1]);
        var validUser = cfg.Users.Any(u => u.Username == username && u.Password == password);
        if (!validUser)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Invalid username or password.");
            return;
        }

        // 2) API Key
        if (!headers.TryGetValue(cfg.ApiKeyHeader, out var apiKey) || string.IsNullOrWhiteSpace(apiKey))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Missing API key.");
            return;
        }
        if (!string.Equals(apiKey, cfg.ExpectedApiKey, StringComparison.Ordinal))
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsync("Invalid API key.");
            return;
        }

        // 3) Dataset Key (strict)
        if (!headers.TryGetValue(cfg.DatasetKeyHeader, out var datasetKey) || string.IsNullOrWhiteSpace(datasetKey))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Missing dataset key.");
            return;
        }
        if (!string.Equals(datasetKey, cfg.ExpectedDatasetKey, StringComparison.Ordinal))
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsync("Invalid dataset key.");
            return;
        }

        await _next(context);
    }
}
