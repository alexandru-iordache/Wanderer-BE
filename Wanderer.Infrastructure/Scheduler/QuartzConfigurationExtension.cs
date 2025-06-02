using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Wanderer.Application.Scheduler;

namespace Wanderer.Infrastructure.Scheduler;

public static class QuartzConfigurationExtension
{
    public static IServiceCollection AddQuartzConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var dbConfig = configuration.GetConnectionString("WandererDBConnection");
        if (dbConfig == null)
        {
            throw new ArgumentNullException(nameof(configuration), "Connection string for WandererDBConnection is not configured.");
        }

        services.AddQuartz(quartz =>
        {
            quartz.UsePersistentStore(config =>
            {
                config.UseBinarySerializer();
                config.UseSqlServer(sqlConfig =>
                {
                    sqlConfig.ConnectionString = dbConfig;
                    sqlConfig.TablePrefix = "QRTZ_";
                });

                config.UseNewtonsoftJsonSerializer();
            });

            quartz.SchedulerId = $"WandererQuartz";
            quartz.SchedulerName = $"WandererQuartz";
            quartz.UseDefaultThreadPool(config => config.MaxConcurrency = 1);
        });

        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
        services.AddTransient<ISchedulerService, SchedulerService>();

        return services;
    }
}