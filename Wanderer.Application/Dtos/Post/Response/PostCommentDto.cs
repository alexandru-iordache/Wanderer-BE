namespace Wanderer.Application.Dtos.Post.Response;

public class PostCommentDto
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public required string ProfileName { get; set; }

    public string? AvatarUrl { get; set; }

    public DateTime CreatedAt { get; set; }

    public required string Content { get; set; }
}
