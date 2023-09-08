using Wanderer.Application.Dtos.User;
using Wanderer.Domain.Models.Users;
using Wanderer.Infrastructure.Mappers.Generics;

namespace Wanderer.Infrastructure.Mappers;

public class UserMapper : GenericMapper<User, UserDto, UserInsertDto>
{
}
