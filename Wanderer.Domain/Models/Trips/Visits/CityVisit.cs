using System.ComponentModel.DataAnnotations.Schema;
using Wanderer.Domain.Models.Locations;

namespace Wanderer.Domain.Models.Trips.Visits;

[NotMapped]
public class CityVisit
{
    public Guid Id { get; set; }

    public DateTime StartDate { get; set; }

    public City City { get; set; }

    public Guid CityId { get; set; }

    public int NumberOfNights { get; set; }

    public ICollection<DayVisit> Days { get; set; }

    public Trip Trip { get; set; }

    public Guid TripId { get; set; }

    public int Order { get; set; }
}
