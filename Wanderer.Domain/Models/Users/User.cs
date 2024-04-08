namespace Wanderer.Domain.Models.Users;

public class User
{
    public User(Guid id, string firstName, string lastName, string address, string email)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Address = address;
        Email = email;
    }

    public Guid Id { get; private set; }

    public string ProfileName { get; private set; }

    public string? FirstName { get; private set; }

    public string? LastName { get; private set; }

    public string? Address { get; private set; }

    public string Email { get; private set; }

    public DateTime BirthDate { get; private set; }
}
