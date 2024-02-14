using Wanderer.Domain.Models.Locations.Places;

namespace Wanderer.Domain.Models.Locations;

public abstract class Waypoint
{
    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public decimal Rating { get; private set; }

    public City City { get; private set; }

    protected Waypoint(Guid id, string name, string description, decimal rating, City city)
    {
        Id = id;
        Name = name;
        Description = description;
        Rating = rating;
        City = city;
    }
}
