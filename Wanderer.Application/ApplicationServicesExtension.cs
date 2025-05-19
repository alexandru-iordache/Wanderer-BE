using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Wanderer.Application.Dtos.User.Request;
using Wanderer.Application.Validators;
using Wanderer.Shared.Constants;

namespace Wanderer.Application;

public static class ApplicationServicesExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        #region Validator Filters
        services.AddKeyedTransient<IAsyncActionFilter, AddUserDtoValidatorFilter>(nameof(AddUserDto));
        services.AddKeyedTransient<IAsyncActionFilter, GetUserTripsValidatorFilter>(HttpContextConstants.ValidatorKeys.GetUserTripsValidator);
        #endregion
        return services;
    }
}
