using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wanderer.Domain.Repositories.Generics;
using Wanderer.Infrastructure.Context;

namespace Wanderer.Infrastructure;

public static class InfrastructureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<WandererDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("WandererDBConnection")),
                             ServiceLifetime.Singleton);

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        var assembly = typeof(WandererDbContext).Assembly;

        IEnumerable<bool> repositoryTypes = assembly.GetTypes().Select(type => type.IsClass &&
                                                                       !type.IsAbstract &&
                                                                       type.GetInterfaces().Any(i => i.IsGenericType &&
                                                                                                     i.GetGenericTypeDefinition() == typeof(IRepository<>)));
        foreach (var type in repositoryTypes)
        {

        }
        return services;
    }
}
