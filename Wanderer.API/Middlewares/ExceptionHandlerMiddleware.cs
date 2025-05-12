namespace Wanderer.API.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            switch (ex) {
                case ArgumentNullException:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync("Bad Request: " + ex.Message);
                    break;
                case UnauthorizedAccessException:
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Unauthorized: " + ex.Message);
                    break;
                case KeyNotFoundException:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    await context.Response.WriteAsync("Not Found: " + ex.Message);
                    break;
                default:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    await context.Response.WriteAsync("Internal Server Error: " + ex.Message);
                    break;
            }
        }
    }
}
