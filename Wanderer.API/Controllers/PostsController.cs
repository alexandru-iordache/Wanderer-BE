using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wanderer.Application.Dtos.Post.Request;
using Wanderer.Application.Services;

namespace Wanderer.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class PostsController : ControllerBase
{
    private readonly IPostService postService;
    private readonly IHttpContextService httpContextService;
    private readonly string uploadsPath;

    public PostsController(IPostService postService, IHttpContextService httpContextService, IWebHostEnvironment hostEnvironment)
    {
        this.postService = postService;
        this.httpContextService = httpContextService;

        uploadsPath = Path.Combine(hostEnvironment.ContentRootPath, "uploads");
        if (!Directory.Exists(uploadsPath))
        {
            Directory.CreateDirectory(uploadsPath);
        }
    }

    [HttpPost("image")]
    public async Task<IActionResult> Upload(IFormFile image)
    {
        var fileName = await postService.SaveImage(image, uploadsPath);

        var url = $"{Request.Scheme}://{Request.Host}/uploads/{fileName}";

        return Ok(new UploadImageResponseDto() { ImageUrl = url });
    }

    [HttpPost()]
    public async Task<IActionResult> CreatePost([FromBody] AddPostDto addPostDto, CancellationToken cancellationToken)
    {
        var userId = httpContextService.GetUserId();

        var response = await postService.CreatePostAsync(addPostDto, userId, cancellationToken);

        return CreatedAtAction(nameof(CreatePost), response);
    }

    [HttpPost("{postId}/comments")]
    public async Task<IActionResult> CreatePostComment(Guid postId, [FromBody] AddPostCommentDto addPostCommentDto, CancellationToken cancellationToken)
    {
        var userId = httpContextService.GetUserId();
        var response = await postService.CreatePostCommentAsync(postId, addPostCommentDto, userId, cancellationToken);

        return CreatedAtAction(nameof(CreatePostComment), response);
    }

    [HttpGet("{postId}/comments")]
    public async Task<IActionResult> GetPostComments(Guid postId, CancellationToken cancellationToken)
    {
        return Ok(await postService.GetPostComments(postId, cancellationToken));
    }

    [HttpPost("{postId}/like")]
    public async Task<IActionResult> ChangePostLikeStatus(Guid postId, CancellationToken cancellationToken)
    {
        var userId = httpContextService.GetUserId();
        
        await postService.ChangePostLikeStatusAsync(postId, userId, cancellationToken);
        return Ok();
    }
}
