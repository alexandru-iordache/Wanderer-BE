namespace Wanderer.Application.Scheduler.Dtos;

public class JobRoutineDto
{
    public bool Repeat { get; set; } = false;

    public string? CronExpression { get; set; }
}
