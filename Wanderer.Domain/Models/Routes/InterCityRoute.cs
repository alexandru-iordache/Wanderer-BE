using Wanderer.Domain.Enums;
using Wanderer.Domain.Models.Locations;
using Wanderer.Domain.Models.Trips;

namespace Wanderer.Domain.Models.Routes;

public class InterCityRoute : Route
{
    public City StartLocation { get; private set; }

    public City EndLocation { get; private set; }

    public InterCityRoute(Guid id, City startLocation, City endLocation, DateTime startDate, DateTime endDate, int distance, TransportType transportType, Trip trip)
        : base(id, startDate, endDate, distance, transportType, RouteType.InterCityRoute, trip)
    {
        StartLocation = startLocation;
        EndLocation = endLocation;
    }
}
