namespace Wanderer.Application.Dtos.Post.Request;

public record UploadImageResponseDto
{
    public required string ImageUrl { get; init; }
}
