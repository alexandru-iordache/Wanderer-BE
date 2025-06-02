using Wanderer.Domain.Models.Posts;
using Wanderer.Infrastructure.Repositories.Generics;

namespace Wanderer.Application.Repositories;

public interface IPostRepository : IRepository<Post>
{
    Task<IEnumerable<PostComment>> GetPostComments(Guid postId);

    Task<PostComment?> GetPostCommentById(Guid commentId);
}
