using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wanderer.Domain.Models.Posts;

namespace Wanderer.Infrastructure.Context.Configurations.Posts;

public class PostLikeConfiguration : IEntityTypeConfiguration<PostLike>
{
    public void Configure(EntityTypeBuilder<PostLike> builder)
    {
        builder.ToTable("POST_LIKES");

        builder.HasKey(x => new { x.PostId, x.UserId });

        builder.HasOne(x => x.User)
           .WithMany(x => x.Likes)
           .HasForeignKey(x => x.UserId)
           .OnDelete(DeleteBehavior.NoAction);
    }
}
