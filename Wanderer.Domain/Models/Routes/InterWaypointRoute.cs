using Wanderer.Domain.Enums;
using Wanderer.Domain.Models.Locations;

namespace Wanderer.Domain.Models.Routes;

public class InterWaypointRoute : Route
{
    public InterWaypointRoute(Guid startLocationId, Guid endLocationId, DateTime startDate, DateTime endDate, int distance, Guid tripId) 
        : base(startLocationId, endLocationId, startDate, endDate, distance, tripId, RouteType.InterWaypoint)
    {
    }

    public Waypoint StartLocation { get; set; }

    public Waypoint EndLocation { get; set; }
}
