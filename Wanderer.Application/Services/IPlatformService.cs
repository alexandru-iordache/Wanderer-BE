using Wanderer.Application.Dtos.Shared;

namespace Wanderer.Application.Services;

public interface IPlatformService
{
    Task<IEnumerable<SearchResultDto>> Search(string searchText);
}
