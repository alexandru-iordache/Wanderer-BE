namespace Wanderer.Application.Dtos.Trip.Request;

public class AddTripDto
{
    public required string Title { get; set; }

    public required string StartDate { get; set; }

    public IEnumerable<AddCityVisitDto> CityVisits { get; set; } = [];
}
