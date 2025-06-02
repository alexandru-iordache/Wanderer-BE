
namespace Wanderer.Application.Services;

public interface IUserFeedService
{
    Task<IEnumerable<Guid>> GetUserFeedAsync(Guid userId);

    Task SetUserFeedAsync(Guid userId, IEnumerable<Guid> postIds);
}
