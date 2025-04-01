using Wanderer.Application.Dtos.Trip.Common;

namespace Wanderer.Application.Dtos.Trip.Request;

public class AddCityVisitDto
{
    public required string StartDate { get; set; }

    public int NumberOfNights { get; set; }

    public required string PlaceId { get; set; }

    public required string City { get; set; }

    public required string Country { get; set; }

    public required LatLngBoundDto NorthEastBound { get; set; }

    public required LatLngBoundDto SouthWestBound { get; set; }

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    public int Order { get; set; }

    public IEnumerable<AddDayVisitDto> DayVisits { get; set; } = [];
}
