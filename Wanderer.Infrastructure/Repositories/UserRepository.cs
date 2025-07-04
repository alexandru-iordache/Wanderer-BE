using Microsoft.EntityFrameworkCore;
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

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(x => x.Email.Equals(email));
    }

    public async Task<IEnumerable<User>> Search(string searchText)
    {
        var pattern = $"%{searchText}%";

        return await _dbSet
            .FromSqlInterpolated($@"
                SELECT * FROM [USERS]
                WHERE [PROFILE_NAME] LIKE {pattern}
                ORDER BY [PROFILE_NAME]
                OFFSET 0 ROWS FETCH NEXT 10 ROWS ONLY
            ")
            .ToListAsync();
    }
}
