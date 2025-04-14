namespace Wanderer.Application.Dtos.Trip.Response;

public class WaypointVisitDto
{
    public Guid Id { get; set; }

    public required string StartTime { get; set; }

    public required string EndTime { get; set; }

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    public required string PlaceId { get; set; }

    public required string Name { get; set; }

    public required string Type { get; set; }
}
