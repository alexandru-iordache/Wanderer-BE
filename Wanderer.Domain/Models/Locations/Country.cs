namespace Wanderer.Domain.Models.Locations;

public class Country
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public ICollection<City> Cities { get; set; } = new List<City>();
}
