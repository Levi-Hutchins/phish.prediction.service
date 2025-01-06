using System.Text;
using Microsoft.Extensions.Options;
using phish.prediction.lib.Features.Cloudflare.Config;

public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IOptions<Authentication> _authSettings;

    public AuthenticationMiddleware(IOptions<Authentication> authSettings, RequestDelegate next)
    {
        _next = next;
        _authSettings = authSettings;
    }

    public async Task Invoke(HttpContext context)
    {
        var authHeader = context.Request.Headers["Authorization"];

        if (authHeader != _authSettings.Value.AuthToken)
        {
            context.Response.StatusCode = 401;
            return;
        }
        await _next.Invoke(context);
    }
}