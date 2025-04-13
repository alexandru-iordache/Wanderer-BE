using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wanderer.Domain.Models.Locations;

namespace Wanderer.Domain.Models.Trips.Visits;

public class CityVisit : BaseEntity
{
    public DateOnly StartDate { get; set; }

    public int NumberOfNights { get; set; }

    public City City { get; set; }

    public Guid CityId { get; set; }

    public ICollection<DayVisit> Days { get; set; }

    public Trip Trip { get; set; }

    public Guid TripId { get; set; }

    public int Order { get; set; }

    [Column("DESCRIPTION")]
    [MaxLength(1000)]
    public string? Description { get; set; }
}
