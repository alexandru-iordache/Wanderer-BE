using Wanderer.Domain.Models.Routes;

namespace Wanderer.Domain.Models.Trips;

public class Trip
{
    public Guid Id { get; private set; }

    public string Title { get; private set; }

    public ICollection<InterCityRoute> InterCityRoutes { get; private set; }

    public ICollection<InterWaypointRoute> InterWaypointRoutes { get; private set; }
}
