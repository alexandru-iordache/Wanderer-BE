using Microsoft.EntityFrameworkCore;
using Wanderer.Application.Repositories;
using Wanderer.Domain.Models.Locations;
using Wanderer.Infrastructure.Context;
using Wanderer.Infrastructure.Repositories.Generics;

namespace Wanderer.Infrastructure.Repositories;

public class CityRepository : Repository<City>, ICityRepository
{
    public CityRepository(WandererDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<City>> GetByPlaceIdList(IEnumerable<string> placeIdList) 
    { 
        return await _dbSet.Include(x => x.Country)
                           .Where(c => placeIdList.Contains(c.PlaceId)).ToListAsync();
    }
}