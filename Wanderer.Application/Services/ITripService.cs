using Wanderer.Application.Dtos.Shared;
using Wanderer.Application.Dtos.Trip.Request;
using Wanderer.Application.Dtos.Trip.Response;

namespace Wanderer.Application.Services;

public interface ITripService
{
    
    Task<IEnumerable<TripDto>> GetUserTrips(Guid userId, FilterOptionsDto filterOptionsDto);

    Task<TripDto?> GetById(Guid id);

    Task<TripDto> InsertTrip(AddTripDto tripInsertDto);

    Task<TripDto> UpdateTrip(Guid id, TripDto tripDto);
    
    Task DeleteTrip(Guid id);

    Task<EmptyResponse> CompleteTrip(Guid id);

    Task<EmptyResponse> PublishTrip(Guid id);
}
