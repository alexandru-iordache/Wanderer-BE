using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Wanderer.Application.Dtos.Trip.Request;
using Wanderer.Application.Dtos.Trip.Response;
using Wanderer.Application.Mappers;
using Wanderer.Application.Repositories;
using Wanderer.Application.Repositories.Constants;
using Wanderer.Application.Services;
using Wanderer.Domain.Models.Locations;
using Wanderer.Domain.Models.Trips;
using Wanderer.Shared.Constants;

namespace Wanderer.Infrastructure.Services;

public class TripService : ITripService
{
    private readonly IMapper mapper;
    private readonly IHttpContextService httpContextService;
    private readonly ITripRepository tripRepository;
    private readonly ICityRepository cityRepository;
    private readonly IWaypointRepository waypointRepository;
    private readonly ICountryRepository countryRepository;
    private readonly ILogger<TripService> logger;

    public TripService(
        IMapper mapper,
        IHttpContextService httpContextService,
        ITripRepository tripRepository,
        ICityRepository cityRepository,
        IWaypointRepository waypointRepository,
        ICountryRepository countryRepository,
        ILogger<TripService> logger)
    {
        this.mapper = mapper;
        this.httpContextService = httpContextService;
        this.tripRepository = tripRepository;
        this.cityRepository = cityRepository;
        this.waypointRepository = waypointRepository;
        this.countryRepository = countryRepository;
        this.logger = logger;
    }

    public async Task<IEnumerable<TripDto>> Get()
    {
        return (await tripRepository.GetAsync()).Select(mapper.Map<TripDto>).ToList();
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

        var cities = await cityRepository.GetByPlaceIdList(tripInsertDto.CityVisits.Select(x => x.PlaceId));
        
        var countryNames = tripInsertDto.CityVisits.Select(x => x.Country);
        await MapCitiesToTrip(countryNames, cities, trip);

        var waypointCityRelations = GetWaypointCityRelations(tripInsertDto);
        await MapWaypointsToTrip(waypointCityRelations, cities, trip);

        await tripRepository.InsertAsync(trip);
        await tripRepository.SaveChangesAsync();

        return mapper.Map<TripDto>(trip);
    }

    public async Task<TripDto> UpdateTrip(Guid id, TripDto tripDto)
    {
        var userId = httpContextService.GetUserId();

        var trip = await tripRepository.GetByIdAsync(id, x => x.OwnerId.Equals(userId), IncludeConstants.TripConstants.IncludeAll) 
                ?? throw new ArgumentException("Trip not found!");
        var tripValueObject = mapper.Map<Trip>(tripDto);

        var cities = await cityRepository.GetByPlaceIdList(tripDto.CityVisits.Select(x => x.PlaceId));

        var countryNames = tripDto.CityVisits.Select(x => x.Country);
        await MapCitiesToTrip(countryNames, cities, tripValueObject);

        var waypointCityRelations = GetWaypointCityRelations(tripDto);
        await MapWaypointsToTrip(waypointCityRelations, cities, tripValueObject);

        trip.UpdateTrip(tripValueObject);
        await tripRepository.SaveChangesAsync();

        return mapper.Map<TripDto>(trip);
    }

    private async Task MapCitiesToTrip(IEnumerable<string> countryNames, IEnumerable<City> cities, Trip trip)
    {
        var countries = await countryRepository.GetByNameList(countryNames);

        var cityPlaceIds = cities.Select(x => x.PlaceId).ToList();
        var countriesNames = countries.Select(x => x.Name).ToList();

        foreach (var cityVisit in trip.CityVisits)
        {
            if (cityPlaceIds.Contains(cityVisit.City.PlaceId))
            {
                cityVisit.City = cities.First(x => x.PlaceId == cityVisit.City.PlaceId);
                continue;
            }

            if (countriesNames.Contains(cityVisit.City.Country.Name))
            {
                cityVisit.City.Country = countries.First(x => x.Name == cityVisit.City.Country.Name);
            }
        }
    }

    private async Task MapWaypointsToTrip(Dictionary<string, string> waypointCityRelations, IEnumerable<City> cities, Trip trip)
    {
        var waypoints = await waypointRepository.GetByPlaceIdList(waypointCityRelations.Keys);

        var waypointVisits = trip.CityVisits.SelectMany(x => x.Days).SelectMany(x => x.WaypointVisits);

        var waypointPlaceIds = waypoints.Select(x => x.PlaceId).ToList();
        foreach (var waypointVisit in waypointVisits)
        {
            if (waypointPlaceIds.Contains(waypointVisit.Waypoint.PlaceId))
            {
                waypointVisit.Waypoint = waypoints.First(x => x.PlaceId == waypointVisit.Waypoint.PlaceId);
                continue;
            }

            var cityPlaceId = waypointCityRelations[waypointVisit.Waypoint.PlaceId];
            if (cities.Any(x => x.PlaceId == cityPlaceId))
            {
                waypointVisit.Waypoint.CityId = cities.First(x => x.PlaceId == waypointCityRelations[waypointVisit.Waypoint.PlaceId]).Id;
                continue;
            }

            waypointVisit.Waypoint.City = trip.CityVisits.First(x => x.City.PlaceId == cityPlaceId).City;
        }
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
