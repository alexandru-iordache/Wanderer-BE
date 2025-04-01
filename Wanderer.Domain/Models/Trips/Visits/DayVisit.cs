namespace Wanderer.Domain.Models.Trips.Visits;

public class DayVisit : BaseEntity
{
    public DateOnly Date { get; set; }

    public ICollection<WaypointVisit> WaypointVisits { get; set; }

    public CityVisit CityVisit { get; set; }

    public Guid CityVisitId { get; set; }
}
