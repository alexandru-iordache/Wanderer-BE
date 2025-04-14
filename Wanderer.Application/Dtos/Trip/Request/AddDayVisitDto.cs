namespace Wanderer.Application.Dtos.Trip.Request;

public class AddDayVisitDto
{
    public required DateTime Date { get; set; }

    public IEnumerable<AddWaypointVisitDto> WaypointVisits { get; set; } = [];
}
