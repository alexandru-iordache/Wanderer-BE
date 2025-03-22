using Wanderer.Domain.Models.Locations;
using Wanderer.Infrastructure.Repositories.Generics;

namespace Wanderer.Application.Repositories;

public interface IWaypointRepository : IRepository<Waypoint>
{
    Task<IEnumerable<Waypoint>> GetByPlaceIdList(IEnumerable<string> placeIdList);
}
