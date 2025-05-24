using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wanderer.Application.Dtos.Post;
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
}
