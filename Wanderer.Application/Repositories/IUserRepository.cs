using Wanderer.Domain.Models.Users;
using Wanderer.Infrastructure.Repositories.Generics;

namespace Wanderer.Application.Repositories;

public interface IUserRepository : IRepository<User>
{
}
