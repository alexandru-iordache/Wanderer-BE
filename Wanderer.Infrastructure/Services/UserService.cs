using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Wanderer.Application.Dtos.User.Common;
using Wanderer.Application.Dtos.User.Request;
using Wanderer.Application.Dtos.User.Response;
using Wanderer.Application.Repositories;
using Wanderer.Application.Services;
using Wanderer.Domain.Models.Users;

namespace Wanderer.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IUserRepository userRepository;
    private readonly IHttpContextService httpContextService;
    private readonly IUserStatsService userStatsService;
    private readonly IMapper mapper;

    public UserService(IUserRepository userRepository, IMapper mapper, IHttpContextService httpContextService, IUserStatsService userStatsService)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
        this.httpContextService = httpContextService;
        this.userStatsService = userStatsService;
    }

    public async Task<IEnumerable<UserDto>> Get()
    {
        return (await userRepository.GetAsync()).Select(mapper.Map<UserDto>).ToList();
    }

    public async Task<UserDto?> GetByFirebaseId(string firebaseId)
    {
        return await userRepository.GetAsync(x => x.FirebaseId == firebaseId)
                                   .ContinueWith(t => t.Result.IsNullOrEmpty() ? null : mapper.Map<UserDto>(t.Result.First()));
    }

    public async Task<UserStatsDto> GetUserStats(bool isCompleted)
    {
        var userId = httpContextService.GetUserId();

        var userStats = await userStatsService.GetUserStats(userId, isCompleted);

        return userStats is not null ? userStats : new UserStatsDto();
    }

    public async Task<UserDto> InsertUser(AddUserDto userInsertDto)
    {
        var firebaseId = httpContextService.GetFirebaseUserId();

        var user = mapper.Map<User>(userInsertDto, opt => opt.Items[nameof(User.FirebaseId)] = firebaseId);
        await userRepository.InsertAsync(user);
        await userRepository.SaveChangesAsync();

        return mapper.Map<UserDto>(user);
    }
}
