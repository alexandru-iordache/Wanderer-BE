using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Wanderer.Application.Dtos.Trip.Request;
using Wanderer.Application.Dtos.Trip.Response;
using Wanderer.Application.Repositories;
using Wanderer.Application.Services.Interfaces;
using Wanderer.Domain.Models.Locations;
using Wanderer.Domain.Models.Trips;

namespace Wanderer.Infrastructure.Services;

public class TripService : ITripService
{
    private readonly IMapper _mapper;
    private readonly ITripRepository _tripRepository;
    private readonly ICityRepository _cityRepository;
    private readonly IWaypointRepository _waypointRepository;
    private readonly ICountryRepository _countryRepository;
    private readonly ILogger<TripService> _logger;

    public TripService(
        IMapper mapper,
        ITripRepository tripRepository,
        ICityRepository cityRepository,
        IWaypointRepository waypointRepository,
        ICountryRepository countryRepository,
        ILogger<TripService> logger)
    {
        _mapper = mapper;
        _tripRepository = tripRepository;
        _cityRepository = cityRepository;
        _waypointRepository = waypointRepository;
        _countryRepository = countryRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<TripDto>> Get()
    {
        return (await _tripRepository.GetAsync()).Select(_mapper.Map<TripDto>).ToList();
    }

    public async Task<TripDto> InsertTrip(AddTripDto tripInsertDto)
    {
        var cities = await _cityRepository.GetByPlaceIdList(tripInsertDto.CityVisits.Select(x => x.PlaceId));

        var trip = _mapper.Map<Trip>(tripInsertDto, opt => opt.Items[nameof(Trip.OwnerId)] = Guid.Parse("62CAD057-6E86-47E3-9700-398DA836B1A1"));

        await MapCitiesToTrip(tripInsertDto, cities, trip);
        await MapWaypointsToTrip(tripInsertDto, cities, trip);

        await _tripRepository.InsertAsync(trip);

        return _mapper.Map<TripDto>(trip);
    }

    private async Task MapWaypointsToTrip(AddTripDto tripInsertDto, IEnumerable<City> cities, Trip trip)
    {
        var waypointCityRelations = GetWaypointCityRelations(tripInsertDto);
        var waypoints = await _waypointRepository.GetByPlaceIdList(waypointCityRelations.Keys);
        
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

    private async Task MapCitiesToTrip(AddTripDto tripInsertDto, IEnumerable<City> cities, Trip trip)
    {
        var countries = await _countryRepository.GetByNameList(tripInsertDto.CityVisits.Select(x => x.Country));
        
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
                    _logger.LogError($"Waypoint {waypointVisit.PlaceId} is part of multiple cities!");
                    throw new Exception("Waypoint cannot be part of multiuple cities!");
                }
            }
        }

        return waypointCityMapping;
    }
}
