namespace Wanderer.Domain.Models.Users;

public class User
{
    public Guid Id { get; private set; }

    public string Email { get; private set; } = string.Empty;

    public string FirstName { get; private set; } = string.Empty;

    public string LastName { get; private set; } = string.Empty;

    public string Address { get; private set; } = string.Empty;

    public string Password { get; private set; } = string.Empty;
}
