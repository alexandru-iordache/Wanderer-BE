using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wanderer.Application.Services;

namespace Wanderer.Application;

public static class ApplicationServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = typeof(UserService).Assembly;

        var serviceTypes = new List<Type>();

        return services;
    }
}
