using Wanderer.Domain.Enums;
using Wanderer.Domain.Models.Locations;

namespace Wanderer.Domain.Models.Routes;

public class InterCityRoute : Route
{
    public InterCityRoute(Guid startLocationId, Guid endLocationId, DateTime startDate, DateTime endDate, int distance, Guid tripId) 
        : base(startLocationId, endLocationId, startDate, endDate, distance, tripId, RouteType.InterCity)
    {
    }

    public City StartLocation { get; set; }

    public City EndLocation { get; set; }
}
