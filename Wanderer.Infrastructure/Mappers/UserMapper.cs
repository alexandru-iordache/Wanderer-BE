using Wanderer.Application.Dtos.User;
using Wanderer.Domain.Models.Users;
using Wanderer.Shared.Mappers;

namespace Wanderer.Application.Mappers;

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

    public override User MapToEntity(UserInsertDto insertDto)
    {
        return new User(
            Guid.NewGuid(), 
            insertDto.FirstName, 
            insertDto.LastName, 
            insertDto.Address, 
            insertDto.Email, 
            insertDto.Password
       );
    }
}
