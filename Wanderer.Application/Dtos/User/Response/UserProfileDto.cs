using Wanderer.Application.Dtos.User.Common;

namespace Wanderer.Application.Dtos.User.Response;

public class UserProfileDto
{
    public required string ProfileName { get; init; }

    public string? ProfileDescription { get; init; }

    public HomeCityDto? HomeCity { get; init; }

    public string? AvatarUrl { get; init; }

    public IEnumerable<string> VisitedCities { get; init; } = new List<string>();

    public IEnumerable<string> VisitedCountries { get; init; } = new List<string>();

    public int FollowersCount { get; init; }

    public int FollowingCount { get; init; }

    public bool IsFollowing { get; init; }
}
