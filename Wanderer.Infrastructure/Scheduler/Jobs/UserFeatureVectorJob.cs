using Newtonsoft.Json;
using Quartz;
using Wanderer.Application.Scheduler.Dtos;
using Wanderer.Application.Scheduler.Dtos.DataDtos;
using Wanderer.Application.Services;

namespace Wanderer.Infrastructure.Scheduler.Jobs;

public class UserFeatureVectorJob : IJob
{
    private readonly IUserFeatureVectorInteractionService userFeatureVectorInteractionService;

    public UserFeatureVectorJob(IUserFeatureVectorInteractionService userFeatureVectorInteractionService)
    {
        this.userFeatureVectorInteractionService = userFeatureVectorInteractionService;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var serializedData = context.MergedJobDataMap.GetString(nameof(ScheduleJobDto.SerializedData));
        if (serializedData == null)
        {
            throw new ArgumentNullException(nameof(serializedData), "Serialized data for UserFeatureVectorJob cannot be null.");
        }

        var dataObject = JsonConvert.DeserializeObject<UserFeatureVectorJobDataDto>(serializedData);

        await userFeatureVectorInteractionService.ComputeUserFeatureVector(dataObject.UserId, dataObject.Published);
    }
}
