namespace Wanderer.Application.Dtos.User.Request;

public record AddUserDto
{
    public required string ProfileName { get; init; }

    public required string Email { get; init; }
}
