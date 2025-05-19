using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wanderer.API.Attributes;
using Wanderer.Application.Dtos.Trip.Request;
using Wanderer.Application.Dtos.User.Request;
using Wanderer.Application.Services;
using Wanderer.Shared.Constants;

namespace Wanderer.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService userService;
    private readonly ITripService tripService;
    private readonly IHttpContextService httpContextService;

    public UsersController(IUserService userService, IHttpContextService httpContextService, ITripService tripService)
    {
        this.userService = userService;
        this.httpContextService = httpContextService;
        this.tripService = tripService;
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

    [HttpGet("{userId}/trips")]
    [Validate(HttpContextConstants.ValidatorKeys.GetUserTripsValidator)]
    public async Task<IActionResult> GetUserTrips(Guid userId, FilterOptionsDto filterOptionsDto)
    {
        return Ok(await tripService.GetUserTrips(userId, filterOptionsDto));
    }

    [HttpPost]
    [Validate]
    public async Task<IActionResult> PostUser([FromBody] AddUserDto userInsertDto)
    {
        return Created(nameof(GetUsers), await userService.InsertUser(userInsertDto));
    }

    [HttpPost("{userId}/follow")]
    public async Task<IActionResult> ChangeFollowingStatus(Guid userId)
    {
        var firebaseId = httpContextService.GetFirebaseUserId();
        return Ok(await userService.ChangeFollowingStatus(firebaseId, userId));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto updateUserDto) 
    {
        return Ok(await userService.UpdateUser(updateUserDto));
    }
}
