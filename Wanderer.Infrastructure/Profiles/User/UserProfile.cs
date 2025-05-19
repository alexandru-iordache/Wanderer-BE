using AutoMapper;
using Wanderer.Application.Dtos.Trip.Common;
using Wanderer.Application.Dtos.User.Common;
using Wanderer.Application.Dtos.User.Request;
using Wanderer.Application.Dtos.User.Response;
using UserModel = Wanderer.Domain.Models.Users.User;

namespace Wanderer.Infrastructure.Profiles.User;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<AddUserDto, UserModel>()
            .ForMember(dest => dest.FirebaseId, opt => opt.MapFrom((src, dest, destMember, ctx) =>
            {
                var firebaseId = ctx.Items[nameof(UserModel.FirebaseId)];
                return firebaseId != null ? (string)firebaseId : throw new InvalidOperationException("FirebaseId is not provided in the mapping context.");
            }));

        CreateMap<UserModel, UserDto>()
            .ForMember(dest => dest.HomeCity, opt => opt.MapFrom((src, dest, destMember, ctx) =>
            {
                return MapHomeCity(src);
            }));

        CreateMap<UserModel, UserProfileDto>()
            .ForMember(dest => dest.HomeCity, opt => opt.MapFrom((src, dest, destMember, ctx) =>
            {
                return MapHomeCity(src);
            }))
            .ForMember(dest => dest.VisitedCities, opt => opt.MapFrom((src, dest, destMember, ctx) =>
            {
                var visitedCities = ctx.Items[nameof(UserProfileDto.VisitedCities)];
                return visitedCities == null
                    ? throw new InvalidOperationException("UserTrips is not provided in the mapping context.")
                    : visitedCities;
            }))
            .ForMember(dest => dest.VisitedCountries, opt => opt.MapFrom((src, dest, destMember, ctx) =>
            {
                var visitedCountries = ctx.Items[nameof(UserProfileDto.VisitedCountries)];
                return visitedCountries == null
                    ? throw new InvalidOperationException("UserTrips is not provided in the mapping context.")
                    : visitedCountries;
            }))
            .ForMember(dest => dest.FollowersCount, opt => opt.MapFrom(src => src.Followers.Count))
            .ForMember(dest => dest.FollowingCount, opt => opt.MapFrom(src => src.Following.Count))
            .ForMember(dest => dest.IsFollowing, opt => opt.MapFrom((src, dest, destMember, ctx) =>
            {
                var isFollowing = ctx.Items[nameof(UserProfileDto.IsFollowing)];
                return isFollowing == null 
                    ? throw new InvalidOperationException("IsFollowing is not provided in the mapping context.")
                    : isFollowing;
            }));
    }

    private static HomeCityDto? MapHomeCity(UserModel src)
    {
        if (src.HomeCity == null)
        {
            return null;
        }

        return new HomeCityDto()
        {
            PlaceId = src.HomeCity.PlaceId,
            City = src.HomeCity.Name,
            Country = src.HomeCity.Country.Name,
            Latitude = src.HomeCity.Latitude,
            Longitude = src.HomeCity.Longitude,
            NorthEastBound = new LatLngBoundDto() { Latitude = src.HomeCity.NorthEastBound.Latitude, Longitude = src.HomeCity.NorthEastBound.Longitude },
            SouthWestBound = new LatLngBoundDto() { Latitude = src.HomeCity.SouthWestBound.Latitude, Longitude = src.HomeCity.SouthWestBound.Longitude },
        };
    }
}
