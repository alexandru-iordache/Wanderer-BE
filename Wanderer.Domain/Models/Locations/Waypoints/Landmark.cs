using Wanderer.Domain.Enums;

namespace Wanderer.Domain.Models.Locations.Places;

public class Landmark : Waypoint
{
    public Landmark(Guid id, string name, string description, decimal rating, Guid cityId) : base(id, name, description, rating, WaypointType.Landmark, cityId)
    {
    }
}
