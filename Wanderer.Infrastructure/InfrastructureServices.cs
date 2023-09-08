using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Wanderer.Domain.Repositories.Generics;
using Wanderer.Infrastructure.Context;
using Wanderer.Infrastructure.Mappers.Generics;

namespace Wanderer.Infrastructure;

public static class InfrastructureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<WandererDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("WandererDBConnection")),
                             ServiceLifetime.Singleton);

        var applicationAssemblies = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.Contains("Wanderer"))
                                                                           .ToDictionary(x => x.FullName.Split(',')[0], x => x);
        if (applicationAssemblies.All(x => x.Key != "Wanderer.Domain"))
        {
            applicationAssemblies.Add("Wanderer.Domain", typeof(Repository<>).Assembly);
        }
        var repositoryTypes = applicationAssemblies["Wanderer.Domain"].GetTypes().Where(type => type.IsClass && !type.IsAbstract &&
                                                                                                type.BaseType != null &&
                                                                                                type.BaseType.IsGenericType &&
                                                                                                type.BaseType.GetGenericTypeDefinition() == typeof(Repository<>)).ToList();

        //Add all repository services
        foreach (var type in repositoryTypes)
        {
            services.AddScoped(type.GetInterfaces().FirstOrDefault(i => i.IsGenericType == false), type);
        }

        var mapperTypes = applicationAssemblies["Wanderer.Infrastructure"].GetTypes().Where(type => type.IsClass && !type.IsAbstract &&
                                                                                                type.BaseType != null &&
                                                                                                type.BaseType.IsGenericType &&
                                                                                                type.BaseType.GetGenericTypeDefinition() == typeof(GenericMapper<,,>)).ToList();
        //Add all mapper services
        foreach (var mapperType in mapperTypes)
        {
            var typeArguments = mapperType.BaseType.GetGenericArguments();

            var genericInterefaceType = typeof(IGenericMapper<,,>).MakeGenericType(typeArguments);

            services.AddScoped(genericInterefaceType, mapperType);
        }

        return services;
    }
}
