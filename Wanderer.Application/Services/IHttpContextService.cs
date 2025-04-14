
namespace Wanderer.Application.Services;

public interface IHttpContextService
{
    string GetFirebaseUserId();
    Guid GetUserId();
}
