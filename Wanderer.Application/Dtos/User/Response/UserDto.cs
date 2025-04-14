namespace Wanderer.Application.Dtos.User.Response;

public record UserDto
{
    public Guid Id { get; init; }

    public required string ProfileName { get; init; }

    public string? Address { get; init; }

    public required string Email { get; init; }
}
