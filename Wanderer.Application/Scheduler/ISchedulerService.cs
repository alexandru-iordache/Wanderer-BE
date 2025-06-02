using Quartz;
using Wanderer.Application.Scheduler.Dtos;

namespace Wanderer.Application.Scheduler;

public interface ISchedulerService
{
    Task ScheduleJob<T>(ScheduleJobDto scheduleJobDto) where T : IJob;
}
