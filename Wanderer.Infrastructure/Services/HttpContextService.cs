using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Http;
using Wanderer.Application.Services;
using Wanderer.Shared.Constants;

namespace Wanderer.Infrastructure.Services;

public class HttpContextService : IHttpContextService
{
    private readonly IHttpContextAccessor contextAccessor;

    public HttpContextService(IHttpContextAccessor contextAccessor)
    {
        this.contextAccessor = contextAccessor;
    }

    public string GetFirebaseUserId()
    {
        var firebaseTokenDetails = contextAccessor.HttpContext.Items["User"] as FirebaseToken;
        if (firebaseTokenDetails == null)
        {
            throw new InvalidOperationException("Call is made from an unauthorized request.");
        }

        return firebaseTokenDetails.Uid;
    }

    public Guid GetUserId()
    {
        var userIdHeader = contextAccessor.HttpContext.Request.Headers[HttpContextConstants.UserIdHeader];
        if (userIdHeader.ToString() == null)
        {
            throw new InvalidOperationException($"{HttpContextConstants.UserIdHeader} header is missing.");
        }

        if (!Guid.TryParse(userIdHeader, out var userId))
        {
            throw new FormatException($"{HttpContextConstants.UserIdHeader} header is not a valid Guid.");
        }

        return userId;
    }
}
