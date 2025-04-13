using AutoMapper;
using System.Globalization;
using Wanderer.Application.Dtos.Trip.Request;
using Wanderer.Application.Dtos.Trip.Response;
using Wanderer.Domain.Models.Trips.Visits;
using Wanderer.Shared.Constants;

namespace Wanderer.Infrastructure.Profiles.Trip;

public class WaypointVisitProfile : Profile
{
    public WaypointVisitProfile()
    {
        CreateMap<AddWaypointVisitDto, WaypointVisit>()
            .ForMember(dest => dest.StartTime, src => src.MapFrom(x => TimeOnly.ParseExact(x.StartTime, FormatConstants.TimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None)))
            .ForMember(dest => dest.EndTime, src => src.MapFrom(x => TimeOnly.ParseExact(x.EndTime, FormatConstants.TimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None)))
            .ForMember(dest => dest.Waypoint, src => src.MapFrom(x => x));

        CreateMap<WaypointVisit, WaypointVisitDto>()
            .ForMember(dest => dest.StartTime, src => src.MapFrom(x => x.StartTime.ToString(FormatConstants.TimeFormat)))
            .ForMember(dest => dest.EndTime, src => src.MapFrom(x => x.EndTime.ToString(FormatConstants.TimeFormat)))
            .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Waypoint.Name))
            .ForMember(dest => dest.Longitude, src => src.MapFrom(x => x.Waypoint.Longitude))
            .ForMember(dest => dest.Latitude, src => src.MapFrom(x => x.Waypoint.Latitude))
            .ForMember(dest => dest.PlaceId, src => src.MapFrom(x => x.Waypoint.PlaceId))
            .ForMember(dest => dest.Type, src => src.MapFrom(x => x.Waypoint.Type));

        CreateMap<WaypointVisitDto, WaypointVisit>()
            .ForMember(dest => dest.StartTime, src => src.MapFrom(x => TimeOnly.ParseExact(x.StartTime, FormatConstants.TimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None)))
            .ForMember(dest => dest.EndTime, src => src.MapFrom(x => TimeOnly.ParseExact(x.EndTime, FormatConstants.TimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None)))
            .ForMember(dest => dest.Waypoint, src => src.MapFrom(x => x));
    }
}
