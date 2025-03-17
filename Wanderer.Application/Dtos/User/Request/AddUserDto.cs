namespace Wanderer.Application.Dtos.User.Request;

public record AddUserDto
{
    public string FirstName { get; init; }

    public string LastName { get; init; }

    public string Address { get; init; }

    public string Email { get; init; }
}
