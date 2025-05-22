using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Wanderer.Application.Repositories;
using Wanderer.Application.Services;
using Wanderer.Infrastructure.Context;
using Wanderer.Infrastructure.Repositories;
using Wanderer.Infrastructure.Services;

namespace Wanderer.Infrastructure;

public static class InfrastructureServicesExtension
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<WandererDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("WandererDBConnection")), ServiceLifetime.Singleton);

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("WandererRedisConnection");
        });

        #region Services
        services.AddScoped<IHttpContextService, HttpContextService>();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITripService, TripService>();
        services.AddTransient<IPostService, PostService>();
        services.AddTransient<IUserStatsService, UserStatsService>();
        #endregion

        #region Repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITripRepository, TripRepository>();
        services.AddScoped<ICityRepository, CityRepository>();
        services.AddScoped<IWaypointRepository, WaypointRepository>();
        services.AddScoped<ICountryRepository, CountryRepository>();
        services.AddScoped<IPostRepository, PostRepository>();
        #endregion

        #region Mappers
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        #endregion

        return services;
    }
}
