using Wanderer.Application.Dtos.User.Common;

namespace Wanderer.Application.Dtos.User.Request;

public record UpdateUserDto
{
    public Guid Id { get; init; }

    public required string ProfileName { get; init; }

    public DateTime? BirthDate { get; init; }

    public string? AvatarUrl { get; init; }

    public HomeCityDto? HomeCity { get; init; }
}
