using Wanderer.Application.Dtos.Shared;
using Wanderer.Application.Dtos.User.Common;
using Wanderer.Application.Dtos.User.Request;
using Wanderer.Application.Dtos.User.Response;

namespace Wanderer.Application.Services;

public interface IUserService
{
    Task<EmptyResponse> ChangeFollowingStatus(string firebaseId, Guid userId);
    
    Task<IEnumerable<UserDto>> Get();
    
    Task<UserDto?> GetByFirebaseId(string firebaseId);
    
    Task<UserProfileDto> GetUserProfile(Guid userId);

    Task<UserStatsDto> GetUserStats(bool isCompleted);

    Task<UserDto> InsertUser(AddUserDto userInsertDto);

    Task<UserDto> UpdateUser(UpdateUserDto updateUserDto);
}