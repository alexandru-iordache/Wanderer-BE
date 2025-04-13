using Wanderer.Application.Dtos.Trip.Response;
using Wanderer.Domain.Models.Trips;

namespace Wanderer.Application.Mappers;

public static class TripMapperExtension
{
    public static void UpdateTrip(this Trip trip, Trip tripValueObject)
    {
        trip.Title = tripValueObject.Title;
        trip.StartDate = tripValueObject.StartDate;
        trip.CityVisits = tripValueObject.CityVisits;
    }
}
