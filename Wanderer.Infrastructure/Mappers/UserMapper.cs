using Wanderer.Application.Dtos.User;
using Wanderer.Domain.Models.Users;
using Wanderer.Shared.Mappers;

namespace Wanderer.Infrastructure.Mappers;

public class UserMapper : BaseMapper<User, UserDto, UserInsertDto>, IBaseMapper<User, UserDto, UserInsertDto>
{
    public override UserDto MapToDto(User entity)
    {
        return new UserDto()
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Email = entity.Email,
            Address = entity.Address
        };
    }

    public override User MapToEntity(UserDto dto)
    {
        throw new NotImplementedException();
    }

    public override User MapToEntity(UserInsertDto insertDto, params object[] parameters)
    {
        var externalUserId = parameters[0].ToString();

        return new User(
            Guid.NewGuid(),
            externalUserId,
            insertDto.ProfileName,
            insertDto.FirstName, 
            insertDto.LastName, 
            insertDto.Address, 
            insertDto.Email
       );
    }
}
