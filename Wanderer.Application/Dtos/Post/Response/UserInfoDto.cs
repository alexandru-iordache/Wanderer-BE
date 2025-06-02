namespace Wanderer.Application.Dtos.Post.Response;

public class UserInfoDto
{
    public Guid Id { get; set; }

    public required string ProfileName { get; set; }

    public string? AvatarUrl { get; set; }
}