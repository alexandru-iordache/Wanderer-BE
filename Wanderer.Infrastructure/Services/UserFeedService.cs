using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Wanderer.Application.Repositories;
using Wanderer.Application.Services;

namespace Wanderer.Infrastructure.Services;

public class UserFeedService : IUserFeedService
{
    private readonly IDistributedCache cache;

    public UserFeedService(IDistributedCache cache)
    {
        this.cache = cache;
    }

    public async Task<IEnumerable<Guid>> GetUserFeedAsync(Guid userId)
    {
        var feedKey = BuildFeedKey(userId);
        var result = await cache.GetStringAsync(feedKey);
        if (result == null)
        {
            return Enumerable.Empty<Guid>();
        }

        return JsonConvert.DeserializeObject<IEnumerable<Guid>>(result) ?? Enumerable.Empty<Guid>();
    }

    public async Task SetUserFeedAsync(Guid userId, IEnumerable<Guid> postIds)
    {
        var feedKey = BuildFeedKey(userId);
        var serializedData = JsonConvert.SerializeObject(postIds);
        await cache.SetStringAsync(feedKey, serializedData);
    }

    private static string BuildFeedKey(Guid userId)
    {
        return $"user_feed_{userId}".ToUpper();
    }
}
