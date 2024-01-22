using Wanderer.Domain.Models.Users;
using Wanderer.Infrastructure.Repositories.Generics;

namespace Wanderer.Infrastructure.Repositories.Interfaces;

public interface IUserRepository : IRepository<User>
{
}
