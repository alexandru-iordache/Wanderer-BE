using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Wanderer.Application.Dtos.User.Common;
using Wanderer.Application.Repositories;
using Wanderer.Application.Repositories.Constants;
using Wanderer.Application.Services;
using Wanderer.Domain.Models.Trips;

namespace Wanderer.Infrastructure.Services;

public class UserStatsService : IUserStatsService
{
    private readonly ITripRepository tripRepository;
    private readonly IDistributedCache cache;

    public UserStatsService(ITripRepository tripRepository, IDistributedCache cache)
    {
        this.tripRepository = tripRepository;
        this.cache = cache;
    }

    public async Task ComputeUserStats(Guid userId, bool isCompleted)
    {
        IEnumerable<Trip> userTrips;
        if (isCompleted)
        {
            userTrips = await tripRepository.GetAsync(t => t.OwnerId == userId && t.Status == Domain.Enums.TripStatus.Completed, includeProperties: IncludeConstants.TripConstants.IncludeAll);
        }
        else
        {
            userTrips = await tripRepository.GetAsync(t => t.OwnerId == userId, includeProperties: IncludeConstants.TripConstants.IncludeAll);
        }

        var userStats = GetComputedUserStats(userTrips);
        var userKey = BuildUserKey(userId, isCompleted);

        await cache.SetStringAsync(userKey, JsonConvert.SerializeObject(userStats));
    }

    public async Task<UserStatsDto?> GetUserStats(Guid userId, bool isCompleted)
    {
        var userKey = BuildUserKey(userId, isCompleted);

        var result = await cache.GetStringAsync(userKey);

        return result == null ? null : JsonConvert.DeserializeObject<UserStatsDto>(result);
    }

    private static UserStatsDto GetComputedUserStats(IEnumerable<Trip> userTrips)
    {
        var tripsCount = userTrips.Count();

        var cities = userTrips.SelectMany(x => x.CityVisits).Select(x => x.City);
        var citiesCount = cities.Distinct().Count();
        var countries = cities.Select(x => x.Country);
        var countriesCount = countries.Distinct().Count();

        var dayVisits = userTrips.SelectMany(x => x.CityVisits).SelectMany(x => x.Days);
        var daysCount = dayVisits.Count();
        var waypointsCount = dayVisits.SelectMany(x => x.WaypointVisits).Select(x => x.Waypoint).Distinct().Count();

        return new UserStatsDto()
        {
            TripsCount = tripsCount,
            CountriesCount = countriesCount,
            CitiesCount = citiesCount,
            WaypointsCount = waypointsCount,
            DaysCount = daysCount,
            CountriesIds = countries
                .GroupBy(c => c.Id)
                .OrderByDescending(g => g.Count())
                .Take(10)
                .Select(g => g.Key)
                .ToList(),
            CitiesIds = cities
                .GroupBy(c => c.Id)
                .OrderByDescending(g => g.Count())
                .Take(10)
                .Select(g => g.Key)
                .ToList(),
            AverageTripLength = (int)userTrips.Select(x => x.CityVisits).Select(x => x.Count).DefaultIfEmpty(0).Average()
        };
    }

    private static string BuildUserKey(Guid userId, bool isCompleted)
    {
        var tripsType = isCompleted ? "completed" : "all";

        return $"user-stats_{userId}_{tripsType}".ToUpper();
    }
}
