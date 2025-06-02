namespace Wanderer.Application.Dtos.Post.Response;

public class PostDto
{
    public Guid Id { get; set; }

    public required string Title { get; set; }
    
    public required string Description { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public Guid OwnerId { get; set; }
    
    public Guid? TripId { get; set; }
    
    public UserInfoDto UserInfo { get; set; }

    public List<PostImageDto> Images { get; set; } = new List<PostImageDto>();

    public bool IsLiked { get; set; }

    public int LikesCount { get; set; }

    public int CommentsCount { get; set; }
}
