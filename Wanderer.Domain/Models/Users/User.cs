using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wanderer.Domain.Models.Locations;
using Wanderer.Domain.Models.Posts;
using Wanderer.Domain.Models.Trips;

namespace Wanderer.Domain.Models.Users;

public class User : BaseEntity
{
    public string ProfileName { get; set; }

    public string FirebaseId { get; set; }

    public string Email { get; set; }

    [Column("PROFILE_DESCRIPTION")]
    [MaxLength(200)]
    public string? ProfileDescription { get; set; }

    public Guid? HomeCityId { get; set; }

    public City? HomeCity { get; set; }

    public ICollection<Trip> Trips { get; set; }

    public ICollection<Post> Posts { get; set; } = new List<Post>();

    public ICollection<PostLike> Likes { get; set; } = new List<PostLike>();

    public ICollection<PostComment> Comments { get; set; } = new List<PostComment>();

    [Column("AVATAR_URL")]
    public string? AvatarUrl { get; set; }

    public ICollection<UserFollower> Followers { get; set; } = new List<UserFollower>();

    public ICollection<UserFollower> Following { get; set; } = new List<UserFollower>();
}
