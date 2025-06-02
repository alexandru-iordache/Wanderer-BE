
using Wanderer.Application.Dtos.User.Common;

namespace Wanderer.Application.Services;

public interface IUserFeatureVectorInteractionService
{
    Task ComputeUserFeatureVector(Guid userId, bool published);
    Task<UserFeatureVectorDto?> GetUserFeatureVector(Guid userId, bool published);
}
