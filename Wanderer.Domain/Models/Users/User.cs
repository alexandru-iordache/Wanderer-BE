using Wanderer.Domain.Models.Trips;

namespace Wanderer.Domain.Models.Users;

public class User
{
    public Guid Id { get; private set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public string Address { get; private set; }

    public string Email { get; private set; }

    public List<Trip> Trips { get; private set; }
}
