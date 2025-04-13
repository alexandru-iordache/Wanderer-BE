using Wanderer.Domain.Models.Trips;
using Wanderer.Domain.Models.Trips.Visits;

namespace Wanderer.Application.Repositories.Constants;

public static class IncludeConstants
{
    public static class TripConstants
    {
        public const string IncludeAll = $"{nameof(Trip.CityVisits)},{nameof(Trip.CityVisits)}.{nameof(CityVisit.City)},{nameof(Trip.CityVisits)}.{nameof(CityVisit.Days)}.{nameof(DayVisit.WaypointVisits)}.{nameof(WaypointVisit.Waypoint)}";
    }
}
