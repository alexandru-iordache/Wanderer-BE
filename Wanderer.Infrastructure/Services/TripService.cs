using AutoMapper;
using LinqKit;
using Microsoft.Extensions.Logging;
using Wanderer.Application.Dtos.Shared;
using Wanderer.Application.Dtos.Trip.Request;
using Wanderer.Application.Dtos.Trip.Response;
using Wanderer.Application.Mappers;
using Wanderer.Application.Repositories;
using Wanderer.Application.Repositories.Constants;
using Wanderer.Application.Services;
using Wanderer.Domain.Enums;
using Wanderer.Domain.Models.Locations;
using Wanderer.Domain.Models.Trips;

namespace Wanderer.Infrastructure.Services;

public class TripService : ITripService
{
    private readonly IMapper mapper;
    private readonly IHttpContextService httpContextService;
    private readonly ITripRepository tripRepository;
    private readonly ICityRepository cityRepository;
    private readonly IWaypointRepository waypointRepository;
    private readonly ICountryRepository countryRepository;
    private readonly IUserStatsService userStatsService;
    private readonly ILogger<TripService> logger;

    public TripService(
        IMapper mapper,
        IHttpContextService httpContextService,
        ITripRepository tripRepository,
        ICityRepository cityRepository,
        IWaypointRepository waypointRepository,
        ICountryRepository countryRepository,
        IUserStatsService userStatsService,
        ILogger<TripService> logger)
    {
        this.mapper = mapper;
        this.httpContextService = httpContextService;
        this.tripRepository = tripRepository;
        this.cityRepository = cityRepository;
        this.waypointRepository = waypointRepository;
        this.countryRepository = countryRepository;
        this.userStatsService = userStatsService;
        this.logger = logger;
    }

    public async Task<IEnumerable<TripDto>> GetUserTrips(Guid userId, FilterOptionsDto filterOptionsDto)
    {
        var filter = PredicateBuilder.New<Trip>(x => x.OwnerId.Equals(userId));

        if (!filterOptionsDto.Status.Equals("All"))
        {
            filter = filter.And(x => x.Status.Equals(Enum.Parse<TripStatus>(filterOptionsDto.Status)));
        }
        if (filterOptionsDto.MinDate != null)
        {
            var dateOnly = DateOnly.FromDateTime(filterOptionsDto.MinDate.Value);
            filter = filter.And(x => x.StartDate >= dateOnly);
        }
        if (filterOptionsDto.MaxDate != null)
        {
            var dateOnly = DateOnly.FromDateTime(filterOptionsDto.MaxDate.Value);
            filter = filter.And(x => x.StartDate <= dateOnly);
        }
        if (filterOptionsDto.IsPublished != null)
        {
            filter = filter.And(x => x.IsPublished == filterOptionsDto.IsPublished);
        }

        var trips = await tripRepository.GetAsync(
            filter: filter,
            orderBy: filterOptionsDto.IsOrderedByDate ? x => x.OrderBy(t => t.StartDate) : null
        );

        return trips.Select(mapper.Map<TripDto>).ToList();
    }

    public async Task<TripDto?> GetById(Guid id)
    {
        var trip = await tripRepository.GetByIdAsync(id, includeProperties: IncludeConstants.TripConstants.IncludeAll);

        return trip == null ? null : mapper.Map<TripDto>(trip);
    }

    public async Task<TripDto> InsertTrip(AddTripDto tripInsertDto)
    {
        var userId = httpContextService.GetUserId();
        var trip = mapper.Map<Trip>(tripInsertDto, opt => opt.Items[nameof(Trip.OwnerId)] = userId);

        var countryNames = tripInsertDto.CityVisits.Select(x => x.Country).Distinct();
        var cityPlaceIds = tripInsertDto.CityVisits.Select(x => x.PlaceId).Distinct();
        var waypointCityRelations = GetWaypointCityRelations(tripInsertDto);

        await BuildTrip(trip, countryNames, cityPlaceIds, waypointCityRelations);

        await tripRepository.InsertAsync(trip);
        await tripRepository.SaveChangesAsync();

        Task.Run(() => userStatsService.ComputeUserStats(userId, false));

        return mapper.Map<TripDto>(trip);
    }

    public async Task<TripDto> UpdateTrip(Guid id, TripDto tripDto)
    {
        var userId = httpContextService.GetUserId();

        var trip = await tripRepository.GetByIdAsync(id, x => x.OwnerId.Equals(userId), IncludeConstants.TripConstants.IncludeAll)
                ?? throw new ArgumentException("Trip not found!");
        var tripValueObject = mapper.Map<Trip>(tripDto);

        var countryNames = tripDto.CityVisits.Select(x => x.Country).Distinct();
        var cityPlaceIds = tripDto.CityVisits.Select(x => x.PlaceId).Distinct();
        var waypointCityRelations = GetWaypointCityRelations(tripDto);

        await BuildTrip(tripValueObject, countryNames, cityPlaceIds, waypointCityRelations);

        trip.UpdateTrip(tripValueObject);
        await tripRepository.SaveChangesAsync();

        Task.Run(() => userStatsService.ComputeUserStats(userId, false));

        return mapper.Map<TripDto>(trip);
    }

    public async Task DeleteTrip(Guid id)
    {
        var userId = httpContextService.GetUserId();
        var trip = await tripRepository.GetByIdAsync(id, x => x.OwnerId.Equals(userId), IncludeConstants.TripConstants.IncludeAll)
                ?? throw new KeyNotFoundException("Trip not found!");

        tripRepository.Delete(trip);
        await tripRepository.SaveChangesAsync();

        Task.Run(() => userStatsService.ComputeUserStats(userId, false));
    }

    public async Task<EmptyResponse> CompleteTrip(Guid id)
    {
        var userId = httpContextService.GetUserId();
        var trip = await tripRepository.GetByIdAsync(id, x => x.OwnerId.Equals(userId))
                ?? throw new KeyNotFoundException("Trip not found!");

        trip.CompleteTrip();
        await tripRepository.SaveChangesAsync();

        // Compute the completed stats, since the not completed ones are the all existing in the db
        Task.Run(() => userStatsService.ComputeUserStats(userId, true));

        return new EmptyResponse();
    }

    public async Task<EmptyResponse> PublishTrip(Guid id)
    {
        var userId = httpContextService.GetUserId();
        var trip = await tripRepository.GetByIdAsync(id, x => x.OwnerId.Equals(userId))
                ?? throw new KeyNotFoundException("Trip not found!");
        
        trip.PublishTrip();
        await tripRepository.SaveChangesAsync();
        
        return new EmptyResponse();
    }

    private async Task BuildTrip(Trip tripValueObject, IEnumerable<string> countryNames, IEnumerable<string> cityPlaceIds, Dictionary<string, string> waypointCityRelations)
    {
        IEnumerable<Country> countries = await GetCountries(tripValueObject, countryNames);

        IEnumerable<City> cities = await GetCities(tripValueObject, cityPlaceIds, countries);

        foreach (var cityVisit in tripValueObject.CityVisits)
        {
            cityVisit.CityId = cities.First(x => x.PlaceId == cityVisit.City.PlaceId).Id;
            cityVisit.City = null!; // reset the object reference so EF Core does try to insert
        }

        IEnumerable<Waypoint> waypoints = await GetWaypoints(tripValueObject, waypointCityRelations, cities);
        var waypointVisits = tripValueObject.CityVisits.SelectMany(x => x.Days).SelectMany(x => x.WaypointVisits);
        foreach (var waypointVisit in waypointVisits)
        {
            waypointVisit.WaypointId = waypoints.First(x => x.PlaceId == waypointVisit.Waypoint.PlaceId).Id;
            waypointVisit.Waypoint = null!; // reset the object reference so EF Core does try to insert
        }
    }

    private async Task<IEnumerable<Country>> GetCountries(Trip trip, IEnumerable<string> countryNames)
    {
        var countries = await countryRepository.GetByNameList(countryNames);
        if (countryNames.Count() != countries.Count())
        {
            var missingCountries = countryNames.Except(countries.Select(x => x.Name));
            var countriesToInsert = trip.CityVisits
                .Select(x => x.City.Country)
                .Where(x => missingCountries.Contains(x.Name))
                .DistinctBy(x => x.Name)
                .ToList();

            await countryRepository.InsertRangeAsync(countriesToInsert);
            await countryRepository.SaveChangesAsync();

            countries = countries.Concat(countriesToInsert);
        }

        return countries;
    }

    private async Task<IEnumerable<City>> GetCities(Trip trip, IEnumerable<string> cityPlaceIds, IEnumerable<Country> countries)
    {
        var cities = await cityRepository.GetByPlaceIdList(cityPlaceIds);
        if (cityPlaceIds.Count() != cities.Count())
        {
            var missingCities = cityPlaceIds.Except(cities.Select(x => x.PlaceId));
            var citiesToInsert = trip.CityVisits
                .Select(x => x.City)
                .Where(x => missingCities.Contains(x.PlaceId))
                .DistinctBy(x => x.PlaceId)
                .ToList();
            citiesToInsert.ForEach(x =>
            {
                x.Country = countries.First(c => c.Name == x.Country.Name);
            });

            await cityRepository.InsertRangeAsync(citiesToInsert);
            await cityRepository.SaveChangesAsync();

            cities = cities.Concat(citiesToInsert);
        }

        return cities;
    }

    private async Task<IEnumerable<Waypoint>> GetWaypoints(Trip trip, Dictionary<string, string> waypointCityRelations, IEnumerable<City> cities)
    {
        var waypoints = await waypointRepository.GetByPlaceIdList(waypointCityRelations.Keys);
        if (waypointCityRelations.Keys.Count != waypoints.Count())
        {
            var missingWaypoints = waypointCityRelations.Keys.Except(waypoints.Select(x => x.PlaceId));
            var waypointsToInsert = trip.CityVisits
                .SelectMany(x => x.Days)
                .SelectMany(x => x.WaypointVisits)
                .Select(x => x.Waypoint)
                .Where(x => missingWaypoints.Contains(x.PlaceId))
                .DistinctBy(x => x.PlaceId)
                .ToList();
            waypointsToInsert.ForEach(x =>
            {
                x.City = cities.First(c => c.PlaceId == waypointCityRelations[x.PlaceId]);
            });

            await waypointRepository.InsertRangeAsync(waypointsToInsert);
            await waypointRepository.SaveChangesAsync();

            waypoints = waypoints.Concat(waypointsToInsert);
        }

        return waypoints;
    }

    private Dictionary<string, string> GetWaypointCityRelations(AddTripDto tripInsertDto)
    {
        var waypointCityMapping = new Dictionary<string, string>();
        foreach (var cityVisitDto in tripInsertDto.CityVisits)
        {
            var cityPlaceId = cityVisitDto.PlaceId;
            var waypointVisitsDto = cityVisitDto.DayVisits.SelectMany(x => x.WaypointVisits).ToList();

            foreach (var waypointVisit in waypointVisitsDto)
            {
                if (waypointCityMapping.TryAdd(waypointVisit.PlaceId, cityPlaceId)) { continue; }

                if (waypointCityMapping[waypointVisit.PlaceId] != cityPlaceId)
                {
                    logger.LogError("Waypoint {PlaceId} is part of multiple cities!", waypointVisit.PlaceId);
                    throw new InvalidOperationException("Waypoint cannot be part of multiuple cities!");
                }
            }
        }

        return waypointCityMapping;
    }

    private Dictionary<string, string> GetWaypointCityRelations(TripDto tripDto)
    {
        var waypointCityMapping = new Dictionary<string, string>();
        foreach (var cityVisitDto in tripDto.CityVisits)
        {
            var cityPlaceId = cityVisitDto.PlaceId;
            var waypointVisitsDto = cityVisitDto.DayVisits.SelectMany(x => x.WaypointVisits).ToList();

            foreach (var waypointVisit in waypointVisitsDto)
            {
                if (waypointCityMapping.TryAdd(waypointVisit.PlaceId, cityPlaceId)) { continue; }

                if (waypointCityMapping[waypointVisit.PlaceId] != cityPlaceId)
                {
                    logger.LogError("Waypoint {PlaceId} is part of multiple cities!", waypointVisit.PlaceId);
                    throw new InvalidOperationException("Waypoint cannot be part of multiuple cities!");
                }
            }
        }

        return waypointCityMapping;
    }
}
