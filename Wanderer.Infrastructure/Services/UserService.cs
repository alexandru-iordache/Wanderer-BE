using Microsoft.AspNetCore.Http;
using Wanderer.Application.Dtos.User;
using Wanderer.Application.Services.Interfaces;
using Wanderer.Domain.Models.Users;
using Wanderer.Infrastructure.Repositories.Interfaces;
using Wanderer.Shared.Extensions;
using Wanderer.Shared.Mappers;

namespace Wanderer.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IBaseMapper<User, UserDto, UserInsertDto> _userMapper;
    private readonly IHttpContextAccessor _contextAccessor;

    public UserService(IUserRepository userRepository, IBaseMapper<User, UserDto, UserInsertDto> userMapper, IHttpContextAccessor contextAccessor)
    {
        _userRepository = userRepository;
        _userMapper = userMapper;
        _contextAccessor=contextAccessor;
    }

    public async Task<IEnumerable<UserDto>> Get()
    {
        return (await _userRepository.GetAsync())
                                     .Select(x => _userMapper.MapToDto(x))
                                     .ToList();
    }

    public async Task<UserDto?> GetByProfileName(string profileName)
    {
        var user = (await _userRepository.GetAsync(x => x.ProfileName == profileName))
                                         .FirstOrDefault();

        if (user == null)
        {
            return null;
        }

        return _userMapper.MapToDto(user);
    }

    public async Task<UserDto?> RegisterUser(UserInsertDto userInsertDto)
    {
        var userExternalId = _contextAccessor.HttpContext.GetUserExternalId();
        if (userExternalId == null)
        {
            // IMPORTANT: Add exception and use the exception handler
            return null;
        }

        var userToInsert = _userMapper.MapToEntity(userInsertDto, [userExternalId]);
        await _userRepository.InsertAsync(userToInsert);

        return _userMapper.MapToDto(userToInsert);
    }
}
