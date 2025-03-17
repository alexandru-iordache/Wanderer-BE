using Wanderer.Domain.Enums;
using Wanderer.Domain.Models.Locations;
using Wanderer.Domain.Models.Trips;

namespace Wanderer.Domain.Models.Routes;

public class InterWaypointRoute : Route
{
    public Waypoint StartLocation { get; private set; }

    public Waypoint EndLocation { get; private set; }

    public InterWaypointRoute(Guid id, Waypoint startLocation, Waypoint endLocation, DateTime startDate, DateTime endDate, int distance, TransportType transportType, Trip trip)
        : base(id, startDate, endDate, distance, transportType, RouteType.InterWaypointRoute, trip)
    {
        StartLocation = startLocation;
        EndLocation = endLocation;
    }
}
