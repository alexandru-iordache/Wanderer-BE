using AutoMapper;
using Wanderer.Application.Dtos.Trip.Request;
using Wanderer.Application.Dtos.Trip.Response;
using Wanderer.Domain.Models.Trips.Visits;

namespace Wanderer.Infrastructure.Profiles.Trip;

public class DayVisitProfile : Profile
{
    public DayVisitProfile()
    {
        CreateMap<AddDayVisitDto, DayVisit>()
            .ForMember(dest => dest.Date, src => src.MapFrom(x => DateOnly.ParseExact(x.Date, "dd/MM/yyyy")))
            .ForMember(dest => dest.WaypointVisits, src => src.MapFrom(x => x.WaypointVisits));

        CreateMap<DayVisit, DayVisitDto>()
            .ForMember(dest => dest.WaypointVisits, src => src.MapFrom(x => x.WaypointVisits));
    }
}
