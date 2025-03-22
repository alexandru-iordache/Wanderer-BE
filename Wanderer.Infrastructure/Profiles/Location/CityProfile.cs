using AutoMapper;
using Wanderer.Application.Dtos.Trip.Common;
using Wanderer.Application.Dtos.Trip.Request;
using Wanderer.Domain.Models.Locations;

namespace Wanderer.Infrastructure.Profiles.Location;

public class CityProfile : Profile
{
	public CityProfile()
	{
		CreateMap<AddCityVisitDto, City>()
			.ForMember(dest => dest.Name, src => src.MapFrom(x => x.City))
			.ForMember(dest => dest.Country, src => src.MapFrom(x => new Country() { Name = x.Country }))
            .ForMember(dest => dest.NorthEastBound, src => src.MapFrom(x => x.NorthEastBound))
            .ForMember(dest => dest.SouthWestBound, src => src.MapFrom(x => x.SouthWestBound));

		CreateMap<LatLngBoundDto, LatLngBound>();

        CreateMap<LatLngBound, LatLngBoundDto>();
    }
}
