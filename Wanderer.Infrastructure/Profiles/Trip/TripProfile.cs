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
            .ForMember(dest => dest.StartDate, src => src.MapFrom(x => DateOnly.ParseExact(x.StartDate, "dd/mm/yyyy")))
            .ForMember(dest => dest.OwnerId, src => src.MapFrom((src, dest, destMember, context) => destMember = (Guid)context.Items[nameof(TripModel.OwnerId)]))
            .ForMember(dest => dest.CityVisits, src => src.MapFrom(x => x.CityVisits));

        CreateMap<TripModel, TripDto>()
            .ForMember(dest => dest.CityVisits, src => src.MapFrom(x => x.CityVisits));
    }
}
