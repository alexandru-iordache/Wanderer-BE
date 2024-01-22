using Microsoft.EntityFrameworkCore;
using Wanderer.Domain.Models.Users;
using Wanderer.Infrastructure.Context;
using Wanderer.Infrastructure.Repositories.Generics;
using Wanderer.Infrastructure.Repositories.Interfaces;

namespace Wanderer.Infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(WandererDbContext dbContext) : base(dbContext)
    {
    }

     
}
