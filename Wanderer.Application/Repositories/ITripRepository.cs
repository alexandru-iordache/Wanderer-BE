using Wanderer.Domain.Models.Trips;
using Wanderer.Infrastructure.Repositories.Generics;

namespace Wanderer.Application.Repositories;

public interface ITripRepository : IRepository<Trip>
{
    Task<IEnumerable<Trip>> GetByOwnerId(Guid userId);
}
