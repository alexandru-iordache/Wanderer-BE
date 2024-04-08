using Wanderer.Domain.Enums;

namespace Wanderer.Domain.Models.Locations.Places;

public class Hotel : Waypoint
{
    public Hotel(Guid id, string name, string description, decimal rating, Guid cityId) : base(id, name, description, rating, WaypointType.Hotel, cityId)
    {
    }
}
