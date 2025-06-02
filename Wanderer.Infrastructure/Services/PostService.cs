using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Data.Entity.Validation;
using Wanderer.Application.Dtos.Post.Request;
using Wanderer.Application.Dtos.Post.Response;
using Wanderer.Application.Mappers;
using Wanderer.Application.Repositories;
using Wanderer.Application.Repositories.Constants;
using Wanderer.Application.Scheduler;
using Wanderer.Application.Scheduler.Dtos.DataDtos;
using Wanderer.Application.Scheduler.Dtos;
using Wanderer.Application.Services;
using Wanderer.Domain.Models.Posts;
using Wanderer.Infrastructure.Scheduler.Jobs;

namespace Wanderer.Infrastructure.Services;

public class PostService : IPostService
{
    private readonly IPostRepository postRepository;
    private readonly ITripRepository tripRepository;
    private readonly IHttpContextService httpContextService;
    private readonly ISchedulerService schedulerService;

    public PostService(IPostRepository postRepository, ITripRepository tripRepository, IHttpContextService httpContextService, ISchedulerService schedulerService)
    {
        this.postRepository = postRepository;
        this.tripRepository = tripRepository;
        this.httpContextService = httpContextService;
        this.schedulerService = schedulerService;
    }

    public async Task<string> SaveImage(IFormFile image, string uploadsPath)
    {
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
        var filePath = Path.Combine(uploadsPath, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await image.CopyToAsync(stream);
        }

        return fileName;
    }

    public async Task<PostDto> CreatePostAsync(AddPostDto addPostDto, Guid userId, CancellationToken cancellationToken)
    {
        if (addPostDto.TripId == null)
        {
            throw new NotImplementedException("TripId is required for creating a post.");
        }

        var trip = await tripRepository.GetByIdAsync(addPostDto.TripId.Value);
        if (trip == null)
        {
            throw new KeyNotFoundException("Trip not found.");
        }

        if (trip.OwnerId != userId)
        {
            throw new UnauthorizedAccessException("You are not authorized to create a post for this trip.");
        }

        if (!trip.IsPublished)
        {
            trip.IsPublished = true;
        }

        await tripRepository.SaveChangesAsync();

        var post = addPostDto.MapToModel(userId);
        await postRepository.InsertAsync(post);
        await postRepository.SaveChangesAsync();

        var addedPost = await postRepository.GetByIdAsync(post.Id, includeProperties: IncludeConstants.PostConstants.IncludeAll);
        if (addedPost == null)
        {
            throw new DbUnexpectedValidationException("The post was not saved or the id is invalid.");
        }

        await ScheduleUserFeatureVectorJob(userId, true);

        return addedPost.MapToDto(userId);
    }

    public async Task<IEnumerable<PostDto>> GetUserPosts(Guid userId)
    {
        var currentUserId = httpContextService.GetUserId();

        var posts = await postRepository.GetAsync(x => x.OwnerId.Equals(userId), includeProperties: IncludeConstants.PostConstants.IncludeAll);

        return posts.Select(x => x.MapToDto(currentUserId));
    }

    public async Task<PostCommentDto> CreatePostCommentAsync(Guid postId, AddPostCommentDto addPostCommentDto, Guid userId, CancellationToken cancellationToken)
    {
        var post = await postRepository.GetByIdAsync(postId);
        if (post == null)
        {
            throw new KeyNotFoundException("Post not found.");
        }

        var postComment = addPostCommentDto.MapToModel(userId);
        post.Comments.Add(postComment);

        await postRepository.SaveChangesAsync();

        var addedPostComment = await postRepository.GetPostCommentById(postComment.Id);
        if (addedPostComment == null)
        {
            throw new DbUnexpectedValidationException("The post comment was not saved or the id is invalid.");
        }

        return addedPostComment.MapToDto();
    }

    public async Task<IEnumerable<PostCommentDto>> GetPostComments(Guid postId, CancellationToken cancellationToken)
    {
        var postComments = await postRepository.GetPostComments(postId);

        return postComments.Select(x => x.MapToDto());
    }

    public async Task ChangePostLikeStatusAsync(Guid postId, Guid userId, CancellationToken cancellationToken)
    {
        var post = await postRepository.GetByIdAsync(postId, includeProperties: $"{nameof(Post.Likes)}");
        if (post == null)
        {
            throw new KeyNotFoundException("Post not found.");
        }

        var postLike = post.Likes.FirstOrDefault(x => x.UserId.Equals(userId));
        if (postLike is not null)
        {
            post.Likes.Remove(postLike);

        }
        else
        {

            var postLikeToAdd = new PostLike
            {
                CreatedAt = DateTime.UtcNow,
                UserId = userId
            };
            post.Likes.Add(postLikeToAdd);
        }

        await postRepository.SaveChangesAsync();
    }

    private async Task ScheduleUserFeatureVectorJob(Guid userId, bool published)
    {
        var userFeatureVectorJobDataDto = new UserFeatureVectorJobDataDto
        {
            UserId = userId,
            Published = published
        };

        var scheduleJobDto = new ScheduleJobDto
        {
            JobDescription = "Compute user stats",
            JobName = "UserFeatureJob",
            JobIdentifier = userId.ToString(),
            StartAt = DateTimeOffset.UtcNow.AddSeconds(1),
            SerializedData = JsonConvert.SerializeObject(userFeatureVectorJobDataDto),
            JobRoutine = new JobRoutineDto() { Repeat = false }
        };

        await schedulerService.ScheduleJob<UserFeatureVectorJob>(scheduleJobDto);
    }
}
