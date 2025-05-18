using Wanderer.Application.Repositories;
using Wanderer.Application.Repositories.Constants;
using Wanderer.Domain.Models.Trips;
using Wanderer.Infrastructure.Context;
using Wanderer.Infrastructure.Repositories.Generics;

namespace Wanderer.Infrastructure.Repositories;

public class TripRepository : Repository<Trip>, ITripRepository
{
    public TripRepository(WandererDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Trip>> GetByOwnerId(Guid userId)
    {
        return await GetAsync(x => x.OwnerId.Equals(userId), includeProperties: IncludeConstants.TripConstants.IncludeAll);
    }
}
