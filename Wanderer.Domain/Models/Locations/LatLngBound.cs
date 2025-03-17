namespace Wanderer.Domain.Models.Locations;

public class LatLngBound
{
    public Guid Id { get; set; }

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    public City City { get; set; }

    public Guid CityId { get; set; }
}
