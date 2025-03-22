using Wanderer.Domain.Models.Locations;
using Wanderer.Infrastructure.Repositories.Generics;

namespace Wanderer.Application.Repositories;

public interface ICityRepository : IRepository<City>
{
    Task<IEnumerable<City>> GetByPlaceIdList(IEnumerable<string> placeIdList);
}
