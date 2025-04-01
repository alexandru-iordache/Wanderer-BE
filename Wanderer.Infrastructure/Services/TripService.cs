using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Wanderer.Application.Dtos.Trip.Request;
using Wanderer.Application.Dtos.Trip.Response;
using Wanderer.Application.Repositories;
using Wanderer.Application.Services;
using Wanderer.Application.Services.Interfaces;
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
        var trip = await tripRepository.GetByIdAsync(id);

        return trip == null ? null : mapper.Map<TripDto>(trip);
    }

    public async Task<TripDto> InsertTrip(AddTripDto tripInsertDto)
    {
        var userId = httpContextService.GetUserId();
        var trip = mapper.Map<Trip>(tripInsertDto, opt => opt.Items[nameof(Trip.OwnerId)] = userId);

        var cities = await cityRepository.GetByPlaceIdList(tripInsertDto.CityVisits.Select(x => x.PlaceId));

        await MapCitiesToTrip(tripInsertDto, cities, trip);
        await MapWaypointsToTrip(tripInsertDto, cities, trip);

        await tripRepository.InsertAsync(trip);

        return mapper.Map<TripDto>(trip);
    }

    private async Task MapCitiesToTrip(AddTripDto tripInsertDto, IEnumerable<City> cities, Trip trip)
    {
        var countries = await countryRepository.GetByNameList(tripInsertDto.CityVisits.Select(x => x.Country));
        
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
                cityVisit.City.CountryId = countries.First(x => x.Name == cityVisit.City.Country.Name).Id;
            }
        }
    }

    private async Task MapWaypointsToTrip(AddTripDto tripInsertDto, IEnumerable<City> cities, Trip trip)
    {
        var waypointCityRelations = GetWaypointCityRelations(tripInsertDto);
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
}
