using System.ComponentModel.DataAnnotations.Schema;
using Wanderer.Domain.Models.Trips;
using Wanderer.Domain.Models.Users;

namespace Wanderer.Domain.Models.Posts;

public class Post : BaseEntity
{
    [Column("TITLE")]
    public string Title { get; set; }

    public User Owner { get; set; }

    public Guid OwnerId { get; set; }

    [Column("DESCRIPTION")]
    public string Description { get; set; }

    public ICollection<PostLike> Likes { get; set; } = new List<PostLike>();

    public ICollection<PostComment> Comments { get; set; } = new List<PostComment>();

    public ICollection<PostImage> Images { get; set; } = new List<PostImage>();

    public Trip? Trip { get; set; }

    public Guid? TripId { get; set; }

    [Column("CREATED_AT")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
