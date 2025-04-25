using Microsoft.AspNetCore.Http;
using Wanderer.Application.Dtos.Trip.Response;
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

    public static void ChangeTripStatus(this Trip trip, bool isCompleted)
    {
        trip.Status = isCompleted ? Domain.Enums.TripStatus.Completed : Domain.Enums.TripStatus.NotCompleted;
    }
}
