using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Wanderer.Application.Dtos.Trip.Request;
using Wanderer.Application.Dtos.User.Common;
using Wanderer.Application.Dtos.User.Request;
using Wanderer.Application.Dtos.User.Response;
using Wanderer.Application.Mappers;
using Wanderer.Application.Repositories;
using Wanderer.Application.Services;
using Wanderer.Domain.Models.Locations;
using Wanderer.Domain.Models.Users;

namespace Wanderer.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IUserRepository userRepository;
    private readonly IHttpContextService httpContextService;
    private readonly IUserStatsService userStatsService;
    private readonly ICityRepository cityRepository;
    private readonly ICountryRepository countryRepository;
    private readonly ITripRepository tripRepository;
    private readonly IMapper mapper;

    public UserService(
        IUserRepository userRepository,
        IMapper mapper,
        IHttpContextService httpContextService,
        IUserStatsService userStatsService,
        ICityRepository cityRepository,
        ICountryRepository countryRepository,
        ITripRepository tripRepository)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
        this.httpContextService = httpContextService;
        this.userStatsService = userStatsService;
        this.cityRepository = cityRepository;
        this.countryRepository = countryRepository;
        this.tripRepository = tripRepository;
    }

    public async Task<IEnumerable<UserDto>> Get()
    {
        return (await userRepository.GetAsync(includeProperties: $"{nameof(User.HomeCity)},{nameof(User.HomeCity)}.{nameof(City.Country)}"))
               .Select(mapper.Map<UserDto>).ToList();
    }

    public async Task<UserDto?> GetByFirebaseId(string firebaseId)
    {
        return await userRepository.GetAsync(x => x.FirebaseId == firebaseId, includeProperties: $"{nameof(User.HomeCity)},{nameof(User.HomeCity)}.{nameof(City.Country)}")
                                   .ContinueWith(t => t.Result.IsNullOrEmpty() ? null : mapper.Map<UserDto>(t.Result.First()));
    }

    public async Task<UserStatsDto> GetUserStats(bool isCompleted)
    {
        var userId = httpContextService.GetUserId();

        var userStats = await userStatsService.GetUserStats(userId, isCompleted);

        return userStats is not null ? userStats : new UserStatsDto();
    }

    public async Task<UserProfileDto> GetUserProfile(Guid userId)
    {
        var user = await userRepository.GetByIdAsync(userId, includeProperties: $"{nameof(User.HomeCity)},{nameof(User.HomeCity)}.{nameof(City.Country)}");
        if (user == null)
        {
            throw new InvalidOperationException("User not found");
        }

        var userTrips = await tripRepository.GetByOwnerId(userId);
        var visitedCities = userTrips.SelectMany(x => x.CityVisits).Select(x => x.City.Name).Distinct();
        var visitedCountries = userTrips.SelectMany(x => x.CityVisits).Select(x => x.City.Country.Name).Distinct();

        return mapper.Map<UserProfileDto>(user, opt =>
        {
            opt.Items[nameof(UserProfileDto.VisitedCities)] = visitedCities;
            opt.Items[nameof(UserProfileDto.VisitedCountries)] = visitedCountries;
        });
    }

    public async Task<UserDto> InsertUser(AddUserDto userInsertDto)
    {
        var firebaseId = httpContextService.GetFirebaseUserId();

        var user = mapper.Map<User>(userInsertDto, opt => opt.Items[nameof(User.FirebaseId)] = firebaseId);
        await userRepository.InsertAsync(user);
        await userRepository.SaveChangesAsync();

        return mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> UpdateUser(UpdateUserDto updateUserDto)
    {
        var user = await userRepository.GetByIdAsync(updateUserDto.Id);
        if (user == null)
        {
            throw new InvalidOperationException("User not found");
        }

        var homeCityId = await GetHomeCityId(updateUserDto);

        user.UpdateUser(updateUserDto, homeCityId);
        await userRepository.SaveChangesAsync();

        var firebaseId = httpContextService.GetFirebaseUserId();

        return (await GetByFirebaseId(firebaseId))!;
    }

    private async Task<Guid?> GetHomeCityId(UpdateUserDto updateUserDto)
    {
        if (updateUserDto.HomeCity == null)
        {
            return null;
        }

        var cities = await cityRepository.GetByPlaceIdList([updateUserDto.HomeCity.PlaceId]);
        if (cities.Any())
        {
            return cities.First().Id;
        }

        Guid countryId;
        var countries = await countryRepository.GetByNameList([updateUserDto.HomeCity.Country]);
        if (countries.Any())
        {
            countryId = countries.First().Id;
        }
        else
        {
            var country = new Country() { Name = updateUserDto.HomeCity.Country };
            await countryRepository.InsertAsync(country);
            await countryRepository.SaveChangesAsync();

            countryId = country.Id;
        }

        var city = new City
        {
            Name = updateUserDto.HomeCity.City,
            PlaceId = updateUserDto.HomeCity.PlaceId,
            Latitude = updateUserDto.HomeCity.Latitude,
            Longitude = updateUserDto.HomeCity.Longitude,
            NorthEastBound = new LatLngBound() { Latitude = updateUserDto.HomeCity.NorthEastBound.Latitude, Longitude = updateUserDto.HomeCity.NorthEastBound.Longitude },
            SouthWestBound = new LatLngBound() { Latitude = updateUserDto.HomeCity.SouthWestBound.Latitude, Longitude = updateUserDto.HomeCity.SouthWestBound.Longitude },
            CountryId = countryId
        };
        await cityRepository.InsertAsync(city);
        await cityRepository.SaveChangesAsync();

        return city.Id;
    }
}
