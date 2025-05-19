using Wanderer.Domain.Models.Trips;

namespace Wanderer.Application.Mappers;

public static class TripExtensions
{
    public static void UpdateTrip(this Trip trip, Trip tripValueObject)
    {
        trip.Title = tripValueObject.Title;
        trip.StartDate = tripValueObject.StartDate;
        trip.CityVisits = tripValueObject.CityVisits;
    }

    public static void CompleteTrip(this Trip trip)
    {
        trip.Status = Domain.Enums.TripStatus.Completed;
    }

    public static void PublishTrip(this Trip trip)
    {
        trip.IsPublished = true;
    }
}
