using Wanderer.Application.Dtos.Shared;
using Wanderer.Application.Repositories;
using Wanderer.Application.Services;

namespace Wanderer.Infrastructure.Services;

public class PlatformService : IPlatformService
{
    private readonly ITripRepository tripRepository;
    private readonly IUserRepository userRepository;

    public PlatformService(ITripRepository tripRepository, IUserRepository userRepository)
    {
        this.tripRepository = tripRepository;
        this.userRepository = userRepository;
    }

    public async Task<IEnumerable<SearchResultDto>> Search(string searchText)
    {
        var tripResults = await tripRepository.Search(searchText);
        var userResults = await userRepository.Search(searchText);

        var searchResults =  tripResults
                .Select(trip => new SearchResultDto
                {
                    Id = trip.Id,
                    Name = trip.Title,
                    Type = "trip"
                })
                .Concat(userResults.Select(user => new SearchResultDto
                {
                    Id = user.Id,
                    AvatarUrl = user.AvatarUrl,
                    Name = user.ProfileName,
                    Type = "user"
                })).OrderBy(x => x.Name).Take(10).ToList();

        return searchResults;
    }
}
