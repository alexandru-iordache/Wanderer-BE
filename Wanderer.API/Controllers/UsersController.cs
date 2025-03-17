using Microsoft.AspNetCore.Mvc;
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

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        return Ok(await _userService.Get());
    }

    [HttpPost]
    public async Task<IActionResult> PostUser([FromBody] AddUserDto userInsertDto)
    {
        return Created(nameof(GetAllUsers), await _userService.InsertUser(userInsertDto));
    }
}
