using AutoMapper;
using Wanderer.Application.Dtos.Trip.Request;
using Wanderer.Application.Dtos.Trip.Response;
using Wanderer.Domain.Models.Locations;
using Wanderer.Domain.Models.Trips.Visits;

namespace Wanderer.Infrastructure.Profiles.Trip;

public class CityVisitProfile : Profile
{
    public CityVisitProfile()
    {
        CreateMap<AddCityVisitDto, CityVisit>()
            .ForMember(dest => dest.StartDate, src => src.MapFrom(x => DateOnly.ParseExact(x.StartDate, "dd/MM/yyyy")))
            .ForMember(dest => dest.Days, src => src.MapFrom(x => x.DayVisits))
            .ForMember(dest => dest.City, src => src.MapFrom(x => x));

        CreateMap<CityVisit, CityVisitDto>()
            .ForMember(dest => dest.City, src => src.MapFrom(x => x.City.Name))
            .ForMember(dest => dest.Country, src => src.MapFrom(x => x.City.Country.Name))
            .ForMember(dest => dest.DayVisits, src => src.MapFrom(x => x.Days))
            .ForMember(dest => dest.NorthEastBound, src => src.MapFrom(x => x.City.NorthEastBound))
            .ForMember(dest => dest.SouthWestBound, src => src.MapFrom(x => x.City.SouthWestBound));
    }
}
