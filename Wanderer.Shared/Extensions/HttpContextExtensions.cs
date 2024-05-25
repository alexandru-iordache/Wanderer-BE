using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Wanderer.Shared.Extensions;

public static class HttpContextExtensions
{
    public static string? GetUserExternalId(this HttpContext context)
    {
        var user = context.User;

        if (user == null || !user.Identity.IsAuthenticated)
        {
            return null;
        }

        var claimsIdentity = user.Identity as ClaimsIdentity;
        var uid = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        return uid;
    }
}
