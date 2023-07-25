using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wanderer.Infrastructure.Context;

namespace Wanderer.Infrastructure;

public static class InfrastructureServices
{ 
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<WandererDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("WandererDb")), 
            ServiceLifetime.Singleton);

        return services;
    }
}
