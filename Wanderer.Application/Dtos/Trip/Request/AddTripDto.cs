namespace Wanderer.Application.Dtos.Trip.Request;

public class AddTripDto
{
    public required string Title { get; set; }

    public required DateTime StartDate { get; set; }

    public IEnumerable<AddCityVisitDto> CityVisits { get; set; } = [];
}
