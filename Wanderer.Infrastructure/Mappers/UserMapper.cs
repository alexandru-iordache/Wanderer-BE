using Wanderer.Application.Dtos.User.Request;
using Wanderer.Application.Dtos.User.Response;
using Wanderer.Domain.Models.Users;
using Wanderer.Shared.Mappers;

namespace Wanderer.Infrastructure.Mappers;

public class UserMapper : BaseMapper<User, UserDto, AddUserDto>, IBaseMapper<User, UserDto, AddUserDto>
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

    public override User MapToEntity(AddUserDto insertDto)
    {
        return new User(
            Guid.NewGuid(), 
            insertDto.FirstName, 
            insertDto.LastName, 
            insertDto.Address, 
            insertDto.Email
       );
    }
}
