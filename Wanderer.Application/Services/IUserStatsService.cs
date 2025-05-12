using Wanderer.Application.Dtos.User.Common;

namespace Wanderer.Application.Services;

public interface IUserStatsService
{
    Task ComputeUserStats(Guid userId, bool isCompleted);

    Task<UserStatsDto?> GetUserStats(Guid userId, bool isCompleted);
}
