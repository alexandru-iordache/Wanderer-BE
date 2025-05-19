using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wanderer.Domain.Models.Users;

namespace Wanderer.Infrastructure.Context.Configurations.Users;

public class UserFollowerConfiguration : IEntityTypeConfiguration<UserFollower>
{
    public void Configure(EntityTypeBuilder<UserFollower> builder)
    {
        builder.ToTable("USER_FOLLOWERS");

        builder.HasKey(x => new { x.UserId, x.FollowerId });

        builder.HasOne(x => x.User)
            .WithMany(x => x.Followers)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Follower)
            .WithMany(x => x.Following)
            .HasForeignKey(x => x.FollowerId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
