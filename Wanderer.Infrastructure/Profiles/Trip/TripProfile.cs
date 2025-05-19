using AutoMapper;
using Wanderer.Application.Dtos.Trip.Request;
using Wanderer.Application.Dtos.Trip.Response;
using TripModel = Wanderer.Domain.Models.Trips.Trip;

namespace Wanderer.Infrastructure.Profiles.Trip;

public class TripProfile : Profile
{
    public TripProfile()
    {
        CreateMap<AddTripDto, TripModel>()
            .ForMember(dest => dest.StartDate, src => src.MapFrom(x => DateOnly.FromDateTime(x.StartDate)))
            .ForMember(dest => dest.OwnerId, src => src.MapFrom((src, dest, destMember, context) => destMember = (Guid)context.Items[nameof(TripModel.OwnerId)]))
            .ForMember(dest => dest.CityVisits, src => src.MapFrom(x => x.CityVisits));

        CreateMap<TripModel, TripDto>()
            .ForMember(dest => dest.StartDate, src => src.MapFrom(x => x.StartDate.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc)))
            .ForMember(dest => dest.CityVisits, src => src.MapFrom(x => x.CityVisits))
            .ForMember(dest => dest.IsCompleted, src => src.MapFrom(x => x.Status == Domain.Enums.TripStatus.Completed ? true : false));

        CreateMap<TripDto, TripModel>()
            .ForMember(dest => dest.StartDate, src => src.MapFrom(x => DateOnly.FromDateTime(x.StartDate)))
            .ForMember(dest => dest.CityVisits, src => src.MapFrom(x => x.CityVisits));
    }
}
