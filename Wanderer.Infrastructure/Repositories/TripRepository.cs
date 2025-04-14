using Wanderer.Application.Repositories;
using Wanderer.Domain.Models.Trips;
using Wanderer.Infrastructure.Context;
using Wanderer.Infrastructure.Repositories.Generics;

namespace Wanderer.Infrastructure.Repositories;

public class TripRepository : Repository<Trip>, ITripRepository
{
    public TripRepository(WandererDbContext dbContext) : base(dbContext)
    {
    }
}
