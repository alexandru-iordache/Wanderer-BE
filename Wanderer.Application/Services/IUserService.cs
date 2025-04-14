using Wanderer.Application.Dtos.User.Request;
using Wanderer.Application.Dtos.User.Response;

namespace Wanderer.Application.Services;

public interface IUserService
{
    Task<IEnumerable<UserDto>> Get();
    
    Task<UserDto?> GetByFirebaseId(string firebaseId);
    
    Task<UserDto> InsertUser(AddUserDto userInsertDto);
}