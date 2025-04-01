using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authorization;
using Wanderer.Shared.Constants;

namespace Wanderer.API.Middlewares;

public class FirebaseAuthenticationMiddleware
{
    private readonly RequestDelegate _next;

    public FirebaseAuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var endpoint = context.GetEndpoint();
        if (!endpoint!.Metadata.Any(x => x is AuthorizeAttribute))
        {
            await _next(context);
        }
        else
        {
            var authHeader = context.Request.Headers["Authorization"].ToString();

            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Authorization header missing or invalid.");
                return;
            }

            var token = authHeader.Substring("Bearer ".Length);

            try
            {
                var decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(token);
                context.Items[HttpContextConstants.FirebaseTokenKey] = decodedToken;
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync($"Token validation failed: {ex.Message}");
            }
        }
    }
}
