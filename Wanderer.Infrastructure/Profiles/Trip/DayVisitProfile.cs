﻿using AutoMapper;
using Wanderer.Application.Dtos.Trip.Request;
using Wanderer.Application.Dtos.Trip.Response;
using Wanderer.Domain.Models.Trips.Visits;

namespace Wanderer.Infrastructure.Profiles.Trip;

public class DayVisitProfile : Profile
{
    public DayVisitProfile()
    {
        CreateMap<AddDayVisitDto, DayVisit>()
            .ForMember(dest => dest.Date, src => src.MapFrom(x => DateOnly.FromDateTime(x.Date)))
            .ForMember(dest => dest.WaypointVisits, src => src.MapFrom(x => x.WaypointVisits));

        CreateMap<DayVisit, DayVisitDto>()
            .ForMember(dest => dest.Date, src => src.MapFrom(x => x.Date.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc)))
            .ForMember(dest => dest.WaypointVisits, src => src.MapFrom(x => x.WaypointVisits));

        CreateMap<DayVisitDto, DayVisit>()
            .ForMember(dest => dest.Date, src => src.MapFrom(x => DateOnly.FromDateTime(x.Date)))
            .ForMember(dest => dest.WaypointVisits, src => src.MapFrom(x => x.WaypointVisits));
    }
}
