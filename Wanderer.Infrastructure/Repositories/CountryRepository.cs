using Microsoft.EntityFrameworkCore;
using Wanderer.Application.Repositories;
using Wanderer.Domain.Models.Locations;
using Wanderer.Infrastructure.Context;
using Wanderer.Infrastructure.Repositories.Generics;

namespace Wanderer.Infrastructure.Repositories;

public class CountryRepository : Repository<Country>, ICountryRepository
{
    public CountryRepository(WandererDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Country>> GetByNameList(IEnumerable<string> nameList)
    {
        return await _dbSet.Where(c => nameList.Contains(c.Name)).ToListAsync();
    }
}
