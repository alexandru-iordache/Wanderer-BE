using System.ComponentModel.DataAnnotations.Schema;

namespace Wanderer.Domain.Models.Posts;

public class PostImage
{
    public Guid PostId { get; set; }

    public Post Post { get; set; }

    [Column("IMAGE_URL")]
    public string ImageUrl { get; set; }

    [Column("CREATED_AT")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}