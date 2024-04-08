using Wanderer.Domain.Enums;
using Wanderer.Domain.Models.Trips;

namespace Wanderer.Domain.Models.Routes;

public abstract class Route
{
    protected Route(Guid startLocationId, Guid endLocationId, DateTime startDate, DateTime endDate, int distance, Guid tripId, RouteType routeType)
    {
        StartLocationId=startLocationId;
        EndLocationId=endLocationId;
        StartDate=startDate;
        EndDate=endDate;
        Distance=distance;
        TripId=tripId;
        RouteType=routeType;
    }

    public RouteType RouteType { get; set; }

    public Guid StartLocationId { get; set; }

    public Guid EndLocationId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int Distance { get; set; }

    public Trip Trip { get; set; }

    public Guid TripId { get; set; }
}
