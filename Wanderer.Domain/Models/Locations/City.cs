using Wanderer.Domain.Models.Places;

namespace Wanderer.Domain.Models.Locations;

public class City
{
    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public Country Country { get; private set; }

    public Guid CountryId { get; private set; }

    public City(Guid id, string name, Country country, Guid countryId)
    {
        Id = id;
        Name = name;
        Country = country;
        CountryId = countryId;
    }

}
