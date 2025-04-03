using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wanderer.API.Attributes;
using Wanderer.Application.Dtos.User.Request;
using Wanderer.Application.Services.Interfaces;

namespace Wanderer.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        return Ok(await _userService.Get());
    }

    [HttpPost]
    [Validate]
    public async Task<IActionResult> PostUser([FromBody] AddUserDto userInsertDto)
    {
        return Created(nameof(GetUsers), await _userService.InsertUser(userInsertDto));
    }
}
