using Wanderer.Domain.Models.Posts;
using Wanderer.Infrastructure.Repositories.Generics;

namespace Wanderer.Application.Repositories;

public interface IPostRepository : IRepository<Post>
{
}
