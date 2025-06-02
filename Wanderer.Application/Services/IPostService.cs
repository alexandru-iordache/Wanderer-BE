using Microsoft.AspNetCore.Http;
using Wanderer.Application.Dtos.Post.Request;
using Wanderer.Application.Dtos.Post.Response;

namespace Wanderer.Application.Services;

public interface IPostService
{
    Task<PostDto> CreatePostAsync(AddPostDto addPostDto, Guid userId, CancellationToken cancellationToken);
    
    Task<PostCommentDto> CreatePostCommentAsync(Guid postId, AddPostCommentDto addPostCommentDto, Guid userId, CancellationToken cancellationToken);

    Task<IEnumerable<PostCommentDto>> GetPostComments(Guid postId, CancellationToken cancellationToken);
    
    Task<IEnumerable<PostDto>> GetUserPosts(Guid userId);

    Task ChangePostLikeStatusAsync(Guid postId, Guid userId, CancellationToken cancellationToken);
    
    Task<string> SaveImage(IFormFile image, string uploadsPath);
}
