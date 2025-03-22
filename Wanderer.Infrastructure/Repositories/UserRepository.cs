using Wanderer.Application.Repositories;
using Wanderer.Domain.Models.Users;
using Wanderer.Infrastructure.Context;
using Wanderer.Infrastructure.Repositories.Generics;

namespace Wanderer.Infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(WandererDbContext dbContext) : base(dbContext)
    {
    }
}
