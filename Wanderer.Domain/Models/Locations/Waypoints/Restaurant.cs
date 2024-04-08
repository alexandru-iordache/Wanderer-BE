using Wanderer.Domain.Enums;


namespace Wanderer.Domain.Models.Locations.Places;

public class Restaurant : Waypoint
{
    public Restaurant(Guid id, string name, string description, decimal rating, Guid cityId) : base(id, name, description, rating, WaypointType.Restaurant, cityId)
    {
    }
}
