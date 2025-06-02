using Wanderer.Application.Dtos.Trip.Request;

namespace Wanderer.Application.Dtos.Trip.Response;

public class TripDto
{
    public Guid Id { get; set; }

    public required string Title { get; set; }

    public required DateTime StartDate { get; set; }

    public IEnumerable<CityVisitDto> CityVisits { get; set; } = [];

    public Guid OwnerId { get; set; }

    public bool IsCompleted { get; set; }

    public bool IsPublished { get; set; }
}
