namespace Wanderer.Application.Dtos.Trip.Response;

public class WaypointVisitDto
{
    public Guid Id { get; set; }

    public required TimeOnly StartTime { get; set; }

    public required TimeOnly EndTime { get; set; }

    public required string PlaceId { get; set; }

    public required string Name { get; set; }

    public required string Type { get; set; }
}
