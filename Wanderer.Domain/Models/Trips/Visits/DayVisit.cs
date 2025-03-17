using System.ComponentModel.DataAnnotations.Schema;
using Wanderer.Domain.Models.Locations;

namespace Wanderer.Domain.Models.Trips.Visits;

[NotMapped]
public class DayVisit
{
    public Guid Id { get; set; }

    public ICollection<WaypointVisit> WaypointVisits { get; set; }

    public CityVisit CityVisit { get; set; }

    public Guid CityVisitId { get; set; }
}
