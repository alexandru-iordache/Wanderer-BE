using System.ComponentModel.DataAnnotations.Schema;
using Wanderer.Domain.Models.Users;

namespace Wanderer.Domain.Models.Posts;

public class PostComment
{
    public Guid Id { get; set; }

    public Guid PostId { get; set; }

    public Post Post { get; set; }

    public Guid UserId { get; set; }

    public User User { get; set; }

    [Column("Content")]
    public string Content { get; set; }

    [Column("CREATED_AT")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}