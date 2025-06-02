using Quartz;
using Quartz.Spi;
using Wanderer.Application.Scheduler;
using Wanderer.Application.Scheduler.Dtos;

namespace Wanderer.Infrastructure.Scheduler;

public class SchedulerService : ISchedulerService
{
    private readonly ISchedulerFactory schedulerFactory;

    public SchedulerService(ISchedulerFactory schedulerFactory)
    {
        this.schedulerFactory = schedulerFactory;
    }

    public async Task ScheduleJob<T>(ScheduleJobDto scheduleJobDto) where T : IJob
    {
        var scheduler = await schedulerFactory.GetScheduler();

        var job = JobBuilder.Create<T>()
                            .WithDescription(scheduleJobDto.JobDescription)
                            .WithIdentity(scheduleJobDto.JobName + scheduleJobDto.JobIdentifier)
                            .UsingJobData(nameof(ScheduleJobDto.SerializedData), scheduleJobDto.SerializedData)
                            .Build();

        var triggerBuilder = TriggerBuilder.Create()
                                    .WithIdentity(scheduleJobDto.JobName + scheduleJobDto.JobIdentifier)
                                    .StartAt(scheduleJobDto.StartAt);

        if (scheduleJobDto.JobRoutine.Repeat)
        {
            triggerBuilder = triggerBuilder.WithCronSchedule(scheduleJobDto.JobRoutine.CronExpression!);
        }

        var trigger = triggerBuilder.Build();

        await scheduler.ScheduleJob(job, trigger);
    }
}
