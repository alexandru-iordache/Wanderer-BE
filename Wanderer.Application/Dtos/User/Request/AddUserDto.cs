namespace Wanderer.Application.Dtos.User.Request;

public record AddUserDto
{
    public required string FirstName { get; init; }

    public required string LastName { get; init; }

    public required string Address { get; init; }

    public required string Email { get; init; }
}
