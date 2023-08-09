using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wanderer.Infrastructure.Context;
using Wanderer.Infrastructure.Repositories.Generics;

namespace Wanderer.Infrastructure;

public static class InfrastructureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<WandererDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("WandererDBConnection")),
                             ServiceLifetime.Singleton);

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        return services;
    }
}
