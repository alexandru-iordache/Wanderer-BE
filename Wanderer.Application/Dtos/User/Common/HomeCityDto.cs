using Wanderer.Application.Dtos.Trip.Common;

namespace Wanderer.Application.Dtos.User.Common;

public record HomeCityDto
{
    public required string PlaceId { get; init; }

    public required string City { get; init; }

    public required string Country { get; init; }

    public required decimal Latitude { get; init; }

    public required decimal Longitude { get; init; }

    public required LatLngBoundDto NorthEastBound { get; set; }

    public required LatLngBoundDto SouthWestBound { get; set; }
}
