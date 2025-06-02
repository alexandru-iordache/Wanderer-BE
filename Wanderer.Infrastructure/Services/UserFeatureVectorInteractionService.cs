using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Wanderer.Application.Dtos.User.Common;
using Wanderer.Application.Repositories;
using Wanderer.Application.Repositories.Constants;
using Wanderer.Application.Services;
using Wanderer.Domain.Models.Trips;

namespace Wanderer.Infrastructure.Services;

public class UserFeatureVectorInteractionService : IUserFeatureVectorInteractionService
{
    private readonly ITripRepository tripRepository;
    private readonly IUserRepository userRepository;
    private readonly IDistributedCache cache;

    public UserFeatureVectorInteractionService(
        ITripRepository tripRepository,
        IUserRepository userRepository,
        IDistributedCache cache)
    {
        this.tripRepository = tripRepository;
        this.cache = cache;
        this.userRepository = userRepository;
    }

    public async Task ComputeUserFeatureVector(Guid userId, bool published)
    {
        var featureVector = new Dictionary<Guid, int>();

        await ComputeCityAndCountryFeatures(featureVector, userId, false);
        await ComputeUserFeatures(featureVector, userId);

        var userKey = BuildUserKey(userId, published);
        var userFeatureVectorDto = new UserFeatureVectorDto
        {
            UserId = userId,
            FeatureVector = featureVector
        };

        await cache.SetStringAsync(userKey, JsonConvert.SerializeObject(userFeatureVectorDto));
    }

    public async Task<UserFeatureVectorDto?> GetUserFeatureVector(Guid userId, bool published)
    {
        var userKey = BuildUserKey(userId, published);
        var result = await cache.GetStringAsync(userKey);
        if (result == null)
        {
            return null;
        }

        return JsonConvert.DeserializeObject<UserFeatureVectorDto>(result);
    }

    private static string BuildUserKey(Guid userId, bool published)
    {
        return $"user_{(published ? "published_" : string.Empty)}feature_vector_{userId}".ToUpper();
    }

    private async Task ComputeCityAndCountryFeatures(Dictionary<Guid, int> featureVector, Guid userId, bool published)
    {
        int skip = 0;
        int top = 30;

        var userTripsCount = !published ? await tripRepository.GetCountAsync(t => t.OwnerId.Equals(userId))
                                        : await tripRepository.GetCountAsync(t => t.OwnerId.Equals(userId) && t.IsPublished && t.Posts.Any());

        do
        {
            IEnumerable<Trip> tripsBatch = [];
            if (!published)
            {
                tripsBatch = await tripRepository.GetBatchAsync(
                                                filter: t => t.OwnerId.Equals(userId),
                                                includeProperties: IncludeConstants.TripConstants.IncludeAll,
                                                skip: skip,
                                                top: top);
            }
            else
            {
                tripsBatch = await tripRepository.GetBatchAsync(
                                                filter: t => t.OwnerId.Equals(userId) && t.IsPublished && t.Posts.Any(),
                                                includeProperties: IncludeConstants.TripConstants.IncludeAll,
                                                skip: skip,
                                                top: top);
            }

            var userCities = tripsBatch.SelectMany(t => t.CityVisits).Select(cv => cv.City).Distinct();
            var userCountries = userCities.Select(c => c.Country).Distinct();

            var cities = tripsBatch
                .SelectMany(t => t.CityVisits)
                .Select(cv => cv.City)
                .Distinct()
                .ToList();

            foreach (var city in cities)
            {
                featureVector.Add(city.Id, 1);
            }

            foreach (var country in userCountries)
            {
                featureVector.Add(country.Id, 1);
            }

            skip += top;
        } while (skip < userTripsCount);
    }

    private async Task ComputeUserFeatures(Dictionary<Guid, int> featureVector, Guid userId)
    {
        var user = (await userRepository.GetByIdAsync(userId, includeProperties: IncludeConstants.UserConstants.IncludeAll))!;

        if (user.HomeCity != null)
        {
            featureVector.TryAdd(user.HomeCity.Id, 1);
        }

        if (user.HomeCity?.Country != null)
        {
            featureVector.TryAdd(user.HomeCity.Country.Id, 1);
        }
    }
}
