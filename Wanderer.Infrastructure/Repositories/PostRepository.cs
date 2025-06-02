using Microsoft.EntityFrameworkCore;
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

    public async Task<PostComment?> GetPostCommentById(Guid commentId)
    {
        return await _dbSet.AsNoTracking()
                        .Include(x => x.Comments)
                        .ThenInclude(x => x.User)
                        .SelectMany(x => x.Comments)
                        .FirstOrDefaultAsync(x => x.Id.Equals(commentId));
    }

    public async Task<IEnumerable<PostComment>> GetPostComments(Guid postId)
    {
        return await _dbSet.AsNoTracking()
                        .Include(x => x.Comments)
                        .ThenInclude(x => x.User)
                        .Where(x => x.Id.Equals(postId))
                        .SelectMany(x => x.Comments)
                        .OrderByDescending(x => x.CreatedAt)
                        .ToListAsync();
    }
}
