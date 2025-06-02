using Newtonsoft.Json;
using Quartz;
using Wanderer.Application.Scheduler.Dtos;
using Wanderer.Application.Scheduler.Dtos.DataDtos;
using Wanderer.Application.Services;

namespace Wanderer.Infrastructure.Scheduler.Jobs;

public class UserStatsJob : IJob
{
    private readonly IUserStatsService userStatsService;

    public UserStatsJob(IUserStatsService userStatsService)
    {
        this.userStatsService = userStatsService;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var serializedData = context.MergedJobDataMap.GetString(nameof(ScheduleJobDto.SerializedData));
        if (serializedData == null)
        {
            throw new ArgumentNullException(nameof(serializedData), "Serialized data for UserStatsJob cannot be null.");
        }

        var dataObject = JsonConvert.DeserializeObject<UserStatsJobDataDto>(serializedData);

        await userStatsService.ComputeUserStats(dataObject.UserId, dataObject.IsCompleted);
    }
}
