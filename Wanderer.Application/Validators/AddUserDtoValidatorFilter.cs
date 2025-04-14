using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Wanderer.Application.Dtos.User.Request;
using Wanderer.Application.Repositories;

namespace Wanderer.Application.Validators;

public class AddUserDtoValidatorFilter : ValidatorFilter<AddUserDto>
{
    private readonly IUserRepository userRepository;

    public AddUserDtoValidatorFilter(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ActionArguments.TryGetValue("userInsertDto", out var value) || value is not AddUserDto addUserDto)
        {
            context.Result = new BadRequestObjectResult("Invalid request.");
            return;
        }

        var user = await userRepository.GetByEmailAsync(addUserDto.Email);
        if (user != null)
        {
            context.Result = new ConflictObjectResult("Email already exists.");
            return;
        }

        await next();
    }
}
