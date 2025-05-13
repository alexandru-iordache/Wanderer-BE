using Wanderer.Application.Dtos.User.Request;
using Wanderer.Domain.Models.Users;

namespace Wanderer.Application.Mappers;

public static class UserExtensions
{
    public static void UpdateUser(this User user, UpdateUserDto userValueObject, Guid? homeCityId = null)
    {
        user.ProfileName = userValueObject.ProfileName;
        user.AvatarUrl = userValueObject.AvatarUrl;
        user.HomeCityId = homeCityId;
    }
}
