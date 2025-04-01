using Wanderer.Application.Dtos.Trip.Request;
using Wanderer.Application.Dtos.Trip.Response;

namespace Wanderer.Application.Services.Interfaces;

public interface ITripService
{
    Task<IEnumerable<TripDto>> Get();

    Task<TripDto?> GetById(Guid id);

    Task<TripDto> InsertTrip(AddTripDto tripInsertDto);
}
