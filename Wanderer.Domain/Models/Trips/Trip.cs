using System.ComponentModel.DataAnnotations.Schema;
using Wanderer.Domain.Enums;
using Wanderer.Domain.Models.Trips.Visits;
using Wanderer.Domain.Models.Users;

namespace Wanderer.Domain.Models.Trips;

public class Trip : BaseEntity
{
    public required string Title { get; set; }

    public DateOnly StartDate { get; set; }

    public User Owner { get; set; }

    public Guid OwnerId { get; set; }

    public ICollection<CityVisit> CityVisits { get; set; } = [];

    [Column("STATUS")]
    public TripStatus Status { get; set; } = TripStatus.NotCompleted;
}
