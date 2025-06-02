using Newtonsoft.Json;
using Quartz;
using Wanderer.Application.Dtos.User.Common;
using Wanderer.Application.Repositories;
using Wanderer.Application.Scheduler.Dtos;
using Wanderer.Application.Scheduler.Dtos.DataDtos;
using Wanderer.Application.Services;

namespace Wanderer.Infrastructure.Scheduler.Jobs;

public class UserFeedJob : IJob
{
    private readonly IUserStatsService userStatsService;
    private readonly IUserRepository userRepository;
    private readonly IPostRepository postRepository;
    private readonly IUserFeatureVectorInteractionService userFeatureVectorInteractionService;
    private readonly IUserFeedService userFeedService;

    public UserFeedJob(
        IUserStatsService userStatsService,
        IUserRepository userRepository,
        IPostRepository postRepository,
        IUserFeatureVectorInteractionService userFeatureVectorInteractionService,
        IUserFeedService userFeedService)
    {
        this.userStatsService = userStatsService;
        this.userRepository = userRepository;
        this.postRepository = postRepository;
        this.userFeatureVectorInteractionService = userFeatureVectorInteractionService;
        this.userFeedService = userFeedService;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var serializedData = context.MergedJobDataMap.GetString(nameof(ScheduleJobDto.SerializedData));
        if (serializedData == null)
        {
            throw new ArgumentNullException(nameof(serializedData), "Serialized data for UserStatsJob cannot be null.");
        }

        var threshold = DateTime.UtcNow.AddDays(-30);
        var dataObject = JsonConvert.DeserializeObject<UserFeedJobDataDto>(serializedData)!;

        var currentUserFeatureVectorDto = await userFeatureVectorInteractionService.GetUserFeatureVector(dataObject.UserId, false);
        if (currentUserFeatureVectorDto == null)
        {
            var trendingPosts = await postRepository.GetBatchAsync(
               filter: x => x.CreatedAt > threshold,
               orderBy: x => x.OrderByDescending(x => x.Likes.Count + x.Comments.Count).ThenByDescending(x => x.CreatedAt),
               skip: 0,
               top: 200);

            await userFeedService.SetUserFeedAsync(dataObject.UserId, trendingPosts.Select(x => x.Id).ToList());
            return;
        }

        Dictionary<Guid, double> userSimiliratyScores = await GetActiveUsersSimiliratyScores(dataObject.UserId, currentUserFeatureVectorDto);

        var followingThreshold = DateTime.UtcNow.AddDays(-7);
        var followingUsersPosts = await postRepository.GetBatchAsync(
            filter: x => x.Owner.Followers.Select(x => x.FollowerId).Contains(x.OwnerId) &&  x.CreatedAt > followingThreshold,
            orderBy: x => x.OrderBy(x => x.Likes.Count + x.Comments.Count).ThenByDescending(x => x.CreatedAt),
            skip: 0,
            top: 100);

        // Maybe enhance the feed by the followingUserPosts comparing them to the feature vector of the current user

        var postIds = followingUsersPosts
            .Select(x => x.Id)
            .ToList();

        var userIdsWithSimilarities = userSimiliratyScores
            .OrderByDescending(x => x.Value)
            .Take(50)
            .Select(x => x.Key)
            .ToList();

        postIds.AddRange(await GetActiveUserPostsIds(userIdsWithSimilarities));
        postIds = postIds.Distinct().ToList();

        if (postIds.Count > 200)
        {
            postIds = postIds.Take(300).ToList();
        }
        else
        {
            var trendingPosts = await postRepository.GetBatchAsync(
                filter: x => !postIds.Contains(x.Id) && x.CreatedAt > threshold,
                orderBy: x => x.OrderByDescending(x => x.Likes.Count + x.Comments.Count).ThenByDescending(x => x.CreatedAt),
                skip: 0,
                top: 300 - postIds.Count);

            postIds.AddRange(trendingPosts.Select(x => x.Id));
        }

        await userFeedService.SetUserFeedAsync(dataObject.UserId, postIds);
    }

    private async Task<IEnumerable<Guid>> GetActiveUserPostsIds(List<Guid> userIdsWithSimilarities)
    {
        var threshold = DateTime.UtcNow.AddDays(-30);
        var activeUsersPostsCount = await postRepository.GetCountAsync(
            filter: x => userIdsWithSimilarities.Contains(x.OwnerId) && x.CreatedAt > threshold);
        var skip = 0;
        var top = 30;

        List<Guid> postIds = [];
        do
        {
            var activeUsersPosts = await postRepository.GetBatchAsync(
                filter: x => userIdsWithSimilarities.Contains(x.OwnerId) && x.CreatedAt > threshold,
                orderBy: x => x.OrderByDescending(x => x.Likes.Count + x.Comments.Count).ThenByDescending(x => x.CreatedAt),
                skip: skip,
                top: top);

            postIds.AddRange(activeUsersPosts.Select(x => x.Id));
            skip += top;
        } while (activeUsersPostsCount > skip);

        return postIds;
    }

    private async Task<Dictionary<Guid, double>> GetActiveUsersSimiliratyScores(Guid userId, UserFeatureVectorDto currentUserFeatureVectorDto)
    {
        var userSimiliratyScores = new Dictionary<Guid, double>();
        var threshold = DateTime.UtcNow.AddDays(-30);
        var usersWithRecentPostsCount = await userRepository
            .GetCountAsync(filter: x => !x.Id.Equals(userId) && x.Posts.Any(p => p.CreatedAt > threshold));
        var skip = 0;
        var top = 30;
        do
        {
            var usersWithRecentPosts = await userRepository.GetBatchAsync(
                filter: x => !x.Id.Equals(userId) && x.Posts.Any(p => p.CreatedAt > threshold),
                orderBy: x => x.OrderByDescending(x => x.Posts.Max(p => p.CreatedAt)),
                skip: skip,
                top: top);

            foreach (var user in usersWithRecentPosts)
            {
                var userPublishedFeatureVector = await userFeatureVectorInteractionService.GetUserFeatureVector(user.Id, true);
                if (userPublishedFeatureVector == null)
                {
                    continue;
                }

                userSimiliratyScores
                    .Add(user.Id, ComputeCosineSimilarity(currentUserFeatureVectorDto.FeatureVector, userPublishedFeatureVector.FeatureVector));
            }

            skip += top;
        } while (usersWithRecentPostsCount > skip);

        return userSimiliratyScores;
    }

    private static double ComputeCosineSimilarity(
        Dictionary<Guid, int> userFeatureVector,
        Dictionary<Guid, int> otherUserFeatureVector)
    {
        var intersection = userFeatureVector.Keys.Intersect(otherUserFeatureVector.Keys);
        if (!intersection.Any())
        {
            return 0.0;
        }

        var dotProduct = intersection.Sum(key => userFeatureVector[key] * otherUserFeatureVector[key]);
        var normA = Math.Sqrt(userFeatureVector.Values.Sum(v => v * v));
        var normB = Math.Sqrt(otherUserFeatureVector.Values.Sum(v => v * v));

        if ((int)normA == 0 || (int)normB == 0)
        {
            return 0.0;
        }

        return dotProduct / (normA * normB);
    }
}
