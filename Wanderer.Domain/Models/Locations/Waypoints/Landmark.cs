namespace Wanderer.Domain.Models.Locations.Places;

public class Landmark : Waypoint
{
    public Landmark(Guid id, string name, string description, decimal rating, City city) : base(id, name, description, rating, city)
    {
    }
}
