using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wanderer.Application.Services;

namespace Wanderer.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class PostsController : ControllerBase
{
    private readonly IPostService postService;
    private readonly IHttpContextService httpContextService;

    public PostsController(IPostService postService, IHttpContextService httpContextService)
    {
        this.postService = postService;
        this.httpContextService = httpContextService;
    }
}
