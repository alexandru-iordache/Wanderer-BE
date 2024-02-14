using Wanderer.Domain.Models.Locations;
using Wanderer.Domain.Models.Locations.Places;

namespace Wanderer.Domain.Models.Places;

public class Country
{
    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public ICollection<City> Cities { get; private set; }

    public Country(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

}
