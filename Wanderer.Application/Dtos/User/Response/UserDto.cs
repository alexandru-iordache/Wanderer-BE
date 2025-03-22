namespace Wanderer.Application.Dtos.User.Response;

public record UserDto
{
    public Guid Id { get; init; }

    public required string FirstName { get; init; }

    public required string LastName { get; init; }

    public required string Address { get; init; }

    public required string Email { get; init; }
}
