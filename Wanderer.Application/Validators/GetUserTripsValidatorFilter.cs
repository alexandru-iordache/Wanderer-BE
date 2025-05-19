using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Wanderer.Application.Dtos.Trip.Request;
using Wanderer.Application.Repositories;
using Wanderer.Application.Services;

namespace Wanderer.Application.Validators;

public class GetUserTripsValidatorFilter : IAsyncActionFilter
{
    private readonly IHttpContextService httpContextService;
    private readonly IUserRepository userRepository;

    public GetUserTripsValidatorFilter(IHttpContextService httpContextService, IUserRepository userRepository)
    {
        this.httpContextService = httpContextService;
        this.userRepository = userRepository;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var firebaseId = httpContextService.GetFirebaseUserId();

        if (!context.ActionArguments.TryGetValue("userId", out var userId))
        {
            context.Result = new BadRequestObjectResult("Invalid request.");
            return;
        }

        if (!context.ActionArguments.TryGetValue("filterOptionsDto", out var filterOptionsObject))
        {
            context.Result = new BadRequestObjectResult("Invalid request.");
            return;
        }

        try
        {
            var filterOptions = (filterOptionsObject as FilterOptionsDto);
            if (filterOptions?.IsPublished != null && filterOptions.IsPublished == false)
            {
                var user = await userRepository.GetByIdAsync((Guid)userId);
                if (user?.FirebaseId != firebaseId)
                {
                    context.Result = new BadRequestObjectResult("You are not authorized to view unpublished trips.");
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            context.Result = new BadRequestObjectResult("Invalid request.");
            return;
        }

        await next();
    }
}
