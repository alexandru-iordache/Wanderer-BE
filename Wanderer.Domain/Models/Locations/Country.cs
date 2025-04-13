namespace Wanderer.Domain.Models.Locations;

public class Country : BaseEntity
{
    public required string Name { get; set; }

    public ICollection<City> Cities { get; set; } = new List<City>();
}
