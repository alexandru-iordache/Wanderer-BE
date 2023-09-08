using Wanderer.Application.Services.Interfaces;
using Wanderer.Domain.Repositories.Interfaces;

namespace Wanderer.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
}
