using System.Linq.Expressions;
using Wanderer.Application.Dtos.User.Request;
using Wanderer.Application.Dtos.User.Response;
using Wanderer.Application.Services.Interfaces;
using Wanderer.Domain.Models.Users;
using Wanderer.Infrastructure.Repositories.Interfaces;
using Wanderer.Shared.Mappers;

namespace Wanderer.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IBaseMapper<User, UserDto, AddUserDto> _userMapper;

    public UserService(IUserRepository userRepository, IBaseMapper<User, UserDto, AddUserDto> userMapper)
    {
        _userRepository = userRepository;
        _userMapper = userMapper;
    }

    public async Task<IEnumerable<UserDto>> Get()
    {
        return (await _userRepository.GetAsync()).Select(x => _userMapper.MapToDto(x)).ToList();
    }

    public async Task<UserDto> InsertUser(AddUserDto userInsertDto)
    {
        var userToInsert = _userMapper.MapToEntity(userInsertDto);
        await _userRepository.InsertAsync(userToInsert);

        return _userMapper.MapToDto(userToInsert);
    }
}
