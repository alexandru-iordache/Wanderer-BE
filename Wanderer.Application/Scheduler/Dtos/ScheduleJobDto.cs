namespace Wanderer.Application.Scheduler.Dtos;

public class ScheduleJobDto
{
    public required string JobDescription { get; set; } = "";

    public required string JobName { get; set; }

    public required string JobIdentifier { get; set; }

    public DateTimeOffset StartAt { get; set; }

    public required JobRoutineDto JobRoutine { get; set; }

    public string? SerializedData { get; set; }
}
