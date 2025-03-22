using AutoMapper;
using Wanderer.Application.Dtos.User.Request;
using Wanderer.Application.Dtos.User.Response;
using Wanderer.Application.Repositories;
using Wanderer.Application.Services.Interfaces;
using Wanderer.Domain.Models.Users;

namespace Wanderer.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserDto>> Get()
    {
        return (await _userRepository.GetAsync()).Select(_mapper.Map<UserDto>).ToList();
    }

    public async Task<UserDto> InsertUser(AddUserDto userInsertDto)
    {
        var user = _mapper.Map<User>(userInsertDto);
        await _userRepository.InsertAsync(user);

        return _mapper.Map<UserDto>(user);
    }
}
