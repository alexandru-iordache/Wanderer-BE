namespace Wanderer.Domain.Models.Users;

public class UserFollower
{
    public Guid UserId { get; set; }

    public User User { get; set; }

    public Guid FollowerId { get; set; }

    public User Follower { get; set; }
}
