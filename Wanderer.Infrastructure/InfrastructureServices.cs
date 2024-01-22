using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wanderer.Application.Dtos.User;
using Wanderer.Application.Mappers;
using Wanderer.Application.Services;
using Wanderer.Application.Services.Interfaces;
using Wanderer.Domain.Models.Users;
using Wanderer.Infrastructure.Context;
using Wanderer.Infrastructure.Repositories;
using Wanderer.Infrastructure.Repositories.Interfaces;
using Wanderer.Shared.Mappers;

namespace Wanderer.Infrastructure;

public static class InfrastructureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<WandererDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("WandererDBConnection")),
                             ServiceLifetime.Singleton);

        #region Services
        services.AddScoped<IUserService, UserService>();
        #endregion

        #region Repositories
        services.AddScoped<IUserRepository, UserRepository>();
        #endregion

        #region Mappers
        services.AddScoped<IBaseMapper<User, UserDto, UserInsertDto>, UserMapper>();
        #endregion

        return services;
    }
}
