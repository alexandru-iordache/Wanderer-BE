using Wanderer.Domain.Models.Trips;

namespace Wanderer.Domain.Models.Routes;

public abstract class Route<T> where T : class
{
    public T StartLocation { get; private set; }

    public T EndLocation { get; private set; }

    public DateTime StartDate { get; private set; }

    public DateTime EndDate { get; private set; }

    public int Distance { get; private set; }

    public Trip Trip { get; private set; }

    public Guid TripId { get; private set; }
}
