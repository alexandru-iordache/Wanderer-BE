using Microsoft.AspNetCore.Mvc.Filters;

namespace Wanderer.API.Attributes;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class ValidateAttribute : Attribute, IAsyncActionFilter
{
    private readonly string? actionFilterKey;

    public ValidateAttribute(string? actionFilterKey = null)
    {
        this.actionFilterKey = actionFilterKey;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        string? parameter = string.Empty;
        if (actionFilterKey == null)
        {
            parameter = context.ActionDescriptor.Parameters
                                                 .Select(x => x.ParameterType.Name)
                                                 .FirstOrDefault(type => type.EndsWith("Dto"));
            if (parameter is null)
            {
                throw new InvalidOperationException("There should be at least one DTO parameter.");
            }
        }
        else
        {
            parameter = actionFilterKey;
        }


        var serviceProvider = context.HttpContext.RequestServices;

        var filter = serviceProvider.GetKeyedService<IAsyncActionFilter>(parameter) ?? throw new InvalidOperationException($"No filter found for type {parameter}");

        await filter.OnActionExecutionAsync(context, next);

    }
}