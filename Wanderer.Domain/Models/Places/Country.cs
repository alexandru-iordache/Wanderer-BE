namespace Wanderer.Domain.Models.Places;

public class Country
{
    public Guid Id { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public ICollection<City> Cities { get; private set; } 

}
