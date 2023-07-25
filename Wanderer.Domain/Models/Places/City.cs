namespace Wanderer.Domain.Models.Places;

public class City
{
    public Guid Id { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public Country  Country { get; private set; }

    public Guid CountryId { get; private set; }
}
