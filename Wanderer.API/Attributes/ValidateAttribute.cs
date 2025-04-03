using Microsoft.AspNetCore.Mvc.Filters;

namespace Wanderer.API.Attributes;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class ValidateAttribute : Attribute, IAsyncActionFilter
{
    public ValidateAttribute()
    {
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var parameters = context.ActionDescriptor.Parameters
                                                 .Select(x => x.ParameterType.Name)
                                                 .Where(type => type.EndsWith("Dto"));
        if (parameters.Count() != 1)
        {
            throw new InvalidOperationException("There should be at least one DTO parameter.");
        }

        var serviceProvider = context.HttpContext.RequestServices;
        foreach(var parameter in parameters) 
        {
            var filter = serviceProvider.GetKeyedService<IAsyncActionFilter>(parameter) ?? throw new InvalidOperationException($"No filter found for type {parameter}");

            await filter.OnActionExecutionAsync(context, next);
        }
    }
}