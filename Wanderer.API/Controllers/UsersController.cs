using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wanderer.Application.Dtos.User;
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

    [HttpGet]
    [Authorize(AuthenticationSchemes = "Firebase")]
    public async Task<IActionResult> GetAllUsers()
    {
        return Ok(await _userService.Get());
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> PostUser([FromBody] UserInsertDto userInsertDto)
    {
        return Created(nameof(GetAllUsers), await _userService.InsertUser(userInsertDto));
    }
}
