using System.ComponentModel.DataAnnotations.Schema;
using Wanderer.Domain.Enums;
using Wanderer.Domain.Models.Trips;

namespace Wanderer.Domain.Models.Routes;

[NotMapped]
public abstract class Route
{
    public Guid Id { get; private set; }

    public DateTime StartDate { get; private set; }

    public DateTime EndDate { get; private set; }

    public int Distance { get; private set; }

    public TransportType TransportType { get; private set; }

    public RouteType RouteType { get; private set; }

    public Trip Trip { get; private set; }

    public Guid TripId { get; private set; }

    protected Route(Guid id, DateTime startDate, DateTime endDate, int distance, TransportType transportType, RouteType routeType, Trip trip)
    {
        Id = id;
        StartDate = startDate;
        EndDate = endDate;
        Distance = distance;
        TransportType = transportType;
        RouteType = routeType;
        Trip = trip;
        TripId = trip.Id;
    }

}
