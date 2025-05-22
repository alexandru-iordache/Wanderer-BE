using Wanderer.Application.Repositories;
using Wanderer.Domain.Models.Posts;
using Wanderer.Infrastructure.Context;
using Wanderer.Infrastructure.Repositories.Generics;

namespace Wanderer.Infrastructure.Repositories;

public class PostRepository : Repository<Post>, IPostRepository
{
    public PostRepository(WandererDbContext dbContext) : base(dbContext)
    {
    }

}
