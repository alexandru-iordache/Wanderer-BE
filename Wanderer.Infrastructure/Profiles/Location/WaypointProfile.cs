using AutoMapper;
using Wanderer.Application.Dtos.Trip.Request;
using Wanderer.Domain.Models.Locations;

namespace Wanderer.Infrastructure.Profiles.Location;

public class WaypointProfile : Profile
{
    public WaypointProfile()
    {
        CreateMap<AddWaypointVisitDto, Waypoint>()
            .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name))
            .ForMember(dest => dest.City, opt => opt.Ignore());
    }
}
