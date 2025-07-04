namespace Wanderer.Application.Dtos.Shared;

public class SearchResultDto
{
    public required Guid Id { get; set; }

    public string? AvatarUrl { get; set; }

    public required string Name { get; set; }

    public required string Type { get; set; }
}
