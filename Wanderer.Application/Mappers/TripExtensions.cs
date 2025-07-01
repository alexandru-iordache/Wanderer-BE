using System.Runtime.CompilerServices;
using Wanderer.Application.Dtos.Trip.Request;
using Wanderer.Domain.Enums;
using Wanderer.Domain.Models.Trips;
using Wanderer.Domain.Models.Trips.Visits;

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

    public static Trip Clone(this Trip trip, Guid userId)
    {
        return new Trip()
        {
            Title = trip.Title,
            StartDate = trip.StartDate,
            OwnerId = userId,
            IsPublished = false,
            Status = TripStatus.NotCompleted,
            CityVisits = trip.CityVisits.Select(x => x.Clone()).ToList()
        };
    }

    private static CityVisit Clone(this CityVisit cityVisit)
    {
        return new CityVisit()
        {
            StartDate = cityVisit.StartDate,
            NumberOfNights = cityVisit.NumberOfNights,
            CityId = cityVisit.CityId,
            Order = cityVisit.Order,
            Days = cityVisit.Days.Select(x => x.Clone()).ToList(),
        };
    }

    private static DayVisit Clone(this DayVisit dayVisit) 
    { 
        return new DayVisit()
        {
            Date = dayVisit.Date,
            WaypointVisits = dayVisit.WaypointVisits.Select(x => x.Clone()).ToList()
        };
    }

    private static WaypointVisit Clone(this WaypointVisit waypointVisit)
    {
        return new WaypointVisit()
        {
            StartTime = waypointVisit.StartTime,
            EndTime = waypointVisit.EndTime,
            WaypointId = waypointVisit.WaypointId
        };
    }
}
