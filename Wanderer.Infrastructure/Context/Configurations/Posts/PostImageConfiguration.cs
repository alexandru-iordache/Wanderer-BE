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

        builder.HasOne(x => x.City)
            .WithMany(x => x.Images)
            .HasPrincipalKey(x => x.PlaceId)
            .HasForeignKey(x => x.CityPlaceId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Waypoint)
            .WithMany(x => x.Images)
            .HasPrincipalKey(x => x.PlaceId)
            .HasForeignKey(x => x.WaypointPlaceId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
