using System.ComponentModel.DataAnnotations.Schema;

namespace Wanderer.Domain.Models.Locations;

[NotMapped]
public class LatLngBound
{
    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }
}
