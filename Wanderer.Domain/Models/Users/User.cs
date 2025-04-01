using Wanderer.Domain.Models.Trips;

namespace Wanderer.Domain.Models.Users;

public class User : BaseEntity
{
    public string ProfileName { get; set; }

    public string Email { get; set; }

    public string? Address { get; set; }

    public ICollection<Trip> Trips { get; set; }
}
