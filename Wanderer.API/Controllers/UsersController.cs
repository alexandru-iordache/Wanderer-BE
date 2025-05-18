using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wanderer.API.Attributes;
using Wanderer.Application.Dtos.User.Request;
using Wanderer.Application.Services;

namespace Wanderer.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService userService;
    private readonly IHttpContextService httpContextService;

    public UsersController(IUserService userService, IHttpContextService httpContextService)
    {
        this.userService = userService;
        this.httpContextService = httpContextService;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        return Ok(await userService.Get());
    }

    [HttpGet("details")]
    public async Task<IActionResult> GetUserDetails()
    {
        var firebaseId = httpContextService.GetFirebaseUserId();

        var userDto = await userService.GetByFirebaseId(firebaseId);
        if (userDto is not null)
        {
            return Ok(userDto);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpGet("{userId}/profile")]
    public async Task<IActionResult> GetUserProfile(Guid userId) 
    { 
       return Ok(await userService.GetUserProfile(userId));
    }

    [HttpGet("stats")]
    public async Task<IActionResult> GetUserStats([FromQuery] bool isCompleted)
    {
        return Ok(await userService.GetUserStats(isCompleted)); 
    }

    [HttpPost]
    [Validate]
    public async Task<IActionResult> PostUser([FromBody] AddUserDto userInsertDto)
    {
        return Created(nameof(GetUsers), await userService.InsertUser(userInsertDto));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto updateUserDto) 
    {
        return Ok(await userService.UpdateUser(updateUserDto));
    }
}
