using Microsoft.EntityFrameworkCore;
using Wanderer.Domain.Models.Users;
using Wanderer.Domain.Repositories.Generics;

namespace Wanderer.Domain.Repositories;

public class UserRepository : Repository<User>
{
    public UserRepository(DbContext dbContext) : base(dbContext)
    {
    }

     
}
