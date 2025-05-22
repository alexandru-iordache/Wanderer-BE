using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wanderer.Domain.Models.Posts;

namespace Wanderer.Infrastructure.Context.Configurations.Posts;

public class PostImageConfiguration : IEntityTypeConfiguration<PostImage>
{
    public void Configure(EntityTypeBuilder<PostImage> builder)
    {
        builder.ToTable("POST_IMAGES");

        builder.HasKey(x => new { x.PostId, x.ImageUrl });
    }
}
