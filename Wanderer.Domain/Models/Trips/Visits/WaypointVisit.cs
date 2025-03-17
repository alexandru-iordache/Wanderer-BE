using System.ComponentModel.DataAnnotations.Schema;
using Wanderer.Domain.Models.Locations;

namespace Wanderer.Domain.Models.Trips.Visits;

public class WaypointVisit
{
    public Guid Id { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public Waypoint Waypoint { get; set; }

    public Guid WaypointId { get; set; }

    public DayVisit DayVisit { get; set; }

    public Guid DayVisitId { get; set; }
}
