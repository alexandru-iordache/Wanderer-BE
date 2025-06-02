namespace Wanderer.Application.Dtos.Post.Request;

public class AddPostDto
{
    public required string Title { get; set; }

    public required string Description { get; set; }

    public Guid? TripId { get; set; }

    public IEnumerable<AddPostImageDto> Images { get; set; } = [];
}
