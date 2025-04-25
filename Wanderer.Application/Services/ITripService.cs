using Wanderer.Application.Dtos.Trip.Request;
using Wanderer.Application.Dtos.Trip.Response;

namespace Wanderer.Application.Services;

public interface ITripService
{
    
    Task<IEnumerable<TripDto>> Get(bool isOrderedByDate);

    Task<TripDto?> GetById(Guid id);

    Task<TripDto> InsertTrip(AddTripDto tripInsertDto);

    Task<TripDto> UpdateTrip(Guid id, TripDto tripDto);
    
    Task DeleteTrip(Guid id);

    Task<TripDto> ChangeTripStatus(Guid id, ChangeTripStatusDto changeTripStatusDto);
}
