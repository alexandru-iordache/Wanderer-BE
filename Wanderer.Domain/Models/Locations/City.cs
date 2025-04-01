using Wanderer.Domain.Models.Trips.Visits;

namespace Wanderer.Domain.Models.Locations;

public class City : BaseEntity
{
    public required string PlaceId { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public Country Country { get; set; }

    public Guid CountryId { get; set; }

    public LatLngBound NorthEastBound { get; set; }

    public LatLngBound SouthWestBound { get; set; }

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    public ICollection<Waypoint> Waypoints { get; set; } = new List<Waypoint>();

    public ICollection<CityVisit> CityVisits { get; set; } = new List<CityVisit>();
}
