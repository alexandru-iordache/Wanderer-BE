namespace Wanderer.Application.Dtos.User.Common;

public class UserStatsDto
{
    public int TripsCount { get; set; } = 0;

    public int CountriesCount { get; set; } = 0;

    public int CitiesCount { get; set; } = 0;

    public int WaypointsCount { get; set; } = 0;

    public int DaysCount { get; set; } = 0;

    public List<Guid> CountriesIds { get; set; } = [];

    public List<Guid> CitiesIds { get; set; } = [];

    public int AverageTripLength { get; set; }
}
