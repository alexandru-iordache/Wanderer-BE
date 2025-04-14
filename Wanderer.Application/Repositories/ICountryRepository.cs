using Wanderer.Domain.Models.Locations;
using Wanderer.Infrastructure.Repositories.Generics;

namespace Wanderer.Application.Repositories;

public interface ICountryRepository : IRepository<Country>
{
    Task<IEnumerable<Country>> GetByNameList(IEnumerable<string> nameList);
}
