using System.ComponentModel.DataAnnotations.Schema;
using Wanderer.Domain.Models.Trips.Visits;
using Wanderer.Domain.Models.Users;

namespace Wanderer.Domain.Models.Trips;

[NotMapped]
public class Trip
{
    public Guid Id { get; private set; }

    public string Title { get; private set; }

    public DateTime StartDate { get; private set; }

    public User Owner { get; private set; }

    public Guid OwnerId { get; private set; }

    public ICollection<CityVisit> CityVisits { get; private set; }
}
