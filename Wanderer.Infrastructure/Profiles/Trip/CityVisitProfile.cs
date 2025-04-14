using AutoMapper;
using Wanderer.Application.Dtos.Trip.Request;
using Wanderer.Application.Dtos.Trip.Response;
using Wanderer.Domain.Models.Trips.Visits;

namespace Wanderer.Infrastructure.Profiles.Trip;

public class CityVisitProfile : Profile
{
    public CityVisitProfile()
    {
        CreateMap<AddCityVisitDto, CityVisit>()
            .ForMember(dest => dest.StartDate, src => src.MapFrom(x => DateOnly.FromDateTime(x.StartDate)))
            .ForMember(dest => dest.Days, src => src.MapFrom(x => x.DayVisits))
            .ForMember(dest => dest.City, src => src.MapFrom(x => x));

        CreateMap<CityVisit, CityVisitDto>()
            .ForMember(dest => dest.StartDate, src => src.MapFrom(x => x.StartDate.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc)))
            .ForMember(dest => dest.City, src => src.MapFrom(x => x.City.Name))
            .ForMember(dest => dest.Country, src => src.MapFrom(x => x.City.Country.Name))
            .ForMember(dest => dest.DayVisits, src => src.MapFrom(x => x.Days))
            .ForMember(dest => dest.NorthEastBound, src => src.MapFrom(x => x.City.NorthEastBound))
            .ForMember(dest => dest.SouthWestBound, src => src.MapFrom(x => x.City.SouthWestBound))
            .ForMember(dest => dest.Latitude, src => src.MapFrom(x => x.City.Latitude))
            .ForMember(dest => dest.Longitude, src => src.MapFrom(x => x.City.Longitude))
            .ForMember(dest => dest.PlaceId, src => src.MapFrom(x => x.City.PlaceId));

        CreateMap<CityVisitDto, CityVisit>()
            .ForMember(dest => dest.StartDate, src => src.MapFrom(x => DateOnly.FromDateTime(x.StartDate)))
            .ForMember(dest => dest.Days, src => src.MapFrom(x => x.DayVisits))
            .ForMember(dest => dest.City, src => src.MapFrom(x => x));
    }
}
