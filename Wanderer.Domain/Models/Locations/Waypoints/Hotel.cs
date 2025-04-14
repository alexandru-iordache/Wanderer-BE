namespace Wanderer.Domain.Models.Locations.Places;

public class Hotel : Waypoint
{
    public Hotel(Guid id, string name, string description, decimal rating, City city) : base(id, name, description, rating, city)
    {
    }
}
