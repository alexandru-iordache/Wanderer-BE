using Wanderer.Application.Dtos.Post;
using Wanderer.Application.Dtos.Post.Request;
using Wanderer.Application.Dtos.Post.Response;
using Wanderer.Application.Dtos.Trip.Response;
using Wanderer.Domain.Models.Posts;

namespace Wanderer.Application.Mappers;

public static class PostExtensions
{
    public static Post MapToModel(this AddPostDto addPostDto, Guid userId)
    {
        return new Post
        {
            Title = addPostDto.Title,
            Description = addPostDto.Description,
            Images = addPostDto.Images.Select(x => x.MapToModel()).ToList(),
            TripId = addPostDto.TripId,
            OwnerId = userId,
            CreatedAt = DateTime.UtcNow
        };
    }

    private static PostImage MapToModel(this AddPostImageDto addPostImageDto)
    {
        return new PostImage()
        {
            ImageUrl = addPostImageDto.ImageUrl,
            CityPlaceId = addPostImageDto.CityPlaceId,
            WaypointPlaceId = addPostImageDto.WaypointPlaceId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static PostDto MapToDto(this Post post, Guid userId)
    {
        return new PostDto
        {
            Id = post.Id,
            Title = post.Title,
            Description = post.Description,
            CreatedAt = post.CreatedAt,
            OwnerId = post.OwnerId,
            TripId = post.TripId,
            Images = post.Images.Select(x => x.MapToDto()).ToList(),
            LikesCount = post.Likes.Count,
            CommentsCount = post.Comments.Count,
            IsLiked = post.Likes.Any(x => x.UserId == userId),
            UserInfo = new UserInfoDto
            {
                Id = post.Owner.Id,
                ProfileName = post.Owner.ProfileName,
                AvatarUrl = post.Owner.AvatarUrl
            }
        };
    }

    public static PostComment MapToModel(this AddPostCommentDto addPostCommentDto, Guid userId)
    {
        return new PostComment
        {
            Content = addPostCommentDto.Content,
            UserId = userId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static PostCommentDto MapToDto(this PostComment postComment)
    {
        return new PostCommentDto
        {
            Id = postComment.Id,
            UserId = postComment.UserId,
            AvatarUrl = postComment.User.AvatarUrl,
            ProfileName = postComment.User.ProfileName,
            Content = postComment.Content,
            CreatedAt = postComment.CreatedAt
        };
    }

    private static PostImageDto MapToDto(this PostImage postImage)
    {
        return new PostImageDto
        {
            ImageUrl = postImage.ImageUrl,
            CityPlaceId = postImage.CityPlaceId,
            WaypointPlaceId = postImage.WaypointPlaceId,
            CityName = postImage.City?.Name,
            WaypointName = postImage.Waypoint?.Name,
            CreatedAt = postImage.CreatedAt
        };
    }
}
