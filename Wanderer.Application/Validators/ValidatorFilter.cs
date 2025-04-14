
using Microsoft.AspNetCore.Mvc.Filters;

namespace Wanderer.Application.Validators;

public abstract class ValidatorFilter<TDto> : IAsyncActionFilter where TDto : class
{
    protected ValidatorFilter()
    {
    }

    public virtual Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        throw new NotImplementedException("This method should be overridden in derived classes.");
    }
}   