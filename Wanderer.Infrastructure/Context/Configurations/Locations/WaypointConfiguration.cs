using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wanderer.Domain.Models.Locations;

namespace Wanderer.Infrastructure.Context.Configurations.Locations;

public class WaypointConfiguration : IEntityTypeConfiguration<Waypoint>
{
    public void Configure(EntityTypeBuilder<Waypoint> builder)
    {
        builder.ToTable("WAYPOINTS");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnType("uniqueidentifier")
            .HasDefaultValueSql("NEWID()")
            .HasColumnName("ID");

        builder.Property(x => x.PlaceId)
            .IsRequired()
            .HasColumnName("PLACE_ID");

        builder.HasIndex(x => x.PlaceId)
            .IsUnique();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnName("NAME");

        builder.Property(x => x.Type)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnName("TYPE");

        builder.HasOne(x => x.City)
            .WithMany(c => c.Waypoints)
            .HasForeignKey(x => x.CityId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}
