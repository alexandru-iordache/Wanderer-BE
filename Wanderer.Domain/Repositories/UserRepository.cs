using Microsoft.EntityFrameworkCore;
using Wanderer.Domain.Models.Users;
using Wanderer.Domain.Repositories.Generics;
using Wanderer.Domain.Repositories.Interfaces;

namespace Wanderer.Domain.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(DbContext dbContext) : base(dbContext)
    {
    }

     
}
