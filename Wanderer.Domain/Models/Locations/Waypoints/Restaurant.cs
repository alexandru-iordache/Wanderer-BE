namespace Wanderer.Domain.Models.Locations.Places;

public class Restaurant : Waypoint
{
    public Restaurant(Guid id, string name, string description, decimal rating, City city) : base(id, name, description, rating, city)
    {
    }
}
