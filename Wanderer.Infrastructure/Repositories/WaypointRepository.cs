using Microsoft.EntityFrameworkCore;
using Wanderer.Application.Repositories;
using Wanderer.Domain.Models.Locations;
using Wanderer.Infrastructure.Context;
using Wanderer.Infrastructure.Repositories.Generics;

namespace Wanderer.Infrastructure.Repositories;

public class WaypointRepository : Repository<Waypoint>, IWaypointRepository
{
    public WaypointRepository(WandererDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Waypoint>> GetByPlaceIdList(IEnumerable<string> placeIdList)
    {
        return await _dbSet.Include(x => x.City)
                           .Where(c => placeIdList.Contains(c.PlaceId)).ToListAsync();
    }
}
