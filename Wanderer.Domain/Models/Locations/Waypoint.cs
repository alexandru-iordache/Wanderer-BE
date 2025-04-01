using Wanderer.Domain.Models.Trips.Visits;

namespace Wanderer.Domain.Models.Locations;

public class Waypoint : BaseEntity
{
    public required string PlaceId { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public City City { get; set; }

    public Guid CityId { get; set; }

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    public ICollection<WaypointVisit> WaypointVisits { get; set; } = new List<WaypointVisit>();

    public required string Type { get; set; }
}
