using Wanderer.Domain.Models.Routes;

namespace Wanderer.Domain.Models.Trips;

public class Trip
{
    public Guid Id { get; private set; }

    public string Title { get; private set; }

    public List<InterCityRoute> InterCityRoutes { get; private set; }

    public List<InterWaypointRoute> InterWaypointRoutes { get; private set; }
}
