namespace Wanderer.Application.Dtos.User;

public record UserInsertDto
{
    public string FirstName { get; init; }

    public string LastName { get; init; }

    public string Address { get; init; }

    public string Email { get; init; }
}
