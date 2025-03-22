using AutoMapper;
using System.Globalization;
using Wanderer.Application.Dtos.Trip.Request;
using Wanderer.Application.Dtos.Trip.Response;
using Wanderer.Domain.Models.Trips.Visits;

namespace Wanderer.Infrastructure.Profiles.Trip;

public class WaypointVisitProfile : Profile
{
    public WaypointVisitProfile()
    {
        CreateMap<AddWaypointVisitDto, WaypointVisit>()
            .ForMember(dest => dest.StartTime, src => src.MapFrom(x => TimeOnly.ParseExact(x.StartTime, "hh:mm", CultureInfo.InvariantCulture, DateTimeStyles.None)))
            .ForMember(dest => dest.EndTime, src => src.MapFrom(x => TimeOnly.ParseExact(x.EndTime, "hh:mm", CultureInfo.InvariantCulture, DateTimeStyles.None)))
            .ForMember(dest => dest.Waypoint, src => src.MapFrom(x => x));

        CreateMap<WaypointVisit, WaypointVisitDto>()
            .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Waypoint.Name));
    }
}
