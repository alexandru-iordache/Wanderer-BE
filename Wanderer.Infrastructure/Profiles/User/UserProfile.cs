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
            }));
    }
}
