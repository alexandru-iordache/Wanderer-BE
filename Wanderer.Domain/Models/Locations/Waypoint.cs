using Wanderer.Domain.Enums;
using Wanderer.Domain.Models.Locations.Places;
using Wanderer.Domain.Models.Routes;

namespace Wanderer.Domain.Models.Locations;

public abstract class Waypoint
{
    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public decimal Rating { get; private set; }

    public WaypointType WaypointType { get; private set; }

    public City City { get; private set; }

    public Guid CityId { get; private set; }

    public ICollection<Route> Routes { get; private set; }

    protected Waypoint(Guid id, string name, string description, decimal rating, WaypointType waypointType, Guid cityId)
    {
        Id = id;
        Name = name;
        Description = description;
        Rating = rating;
        WaypointType=waypointType;
        CityId=cityId;
    }
}
