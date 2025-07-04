using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wanderer.Application.Services;

namespace Wanderer.API.Controllers;

//[Authorize]
[Route("api/[controller]")]
[ApiController]
public class PlatformController : ControllerBase
{
    private readonly IPlatformService platformService;

    public PlatformController(IPlatformService platformService)
    {
        this.platformService = platformService;
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery]string searchText)
    {
        if (string.IsNullOrWhiteSpace(searchText))
        {
            return BadRequest("Search text cannot be empty.");
        }

        var results = await platformService.Search(searchText);
        return Ok(results);
    }
}
