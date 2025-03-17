using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wanderer.Domain.Models.Trips.Visits;

namespace Wanderer.Infrastructure.Context.Configurations.Trips.Visits;

public class WaypointVisitConfiguration : IEntityTypeConfiguration<WaypointVisit>
{
    public void Configure(EntityTypeBuilder<WaypointVisit> builder)
    {
        builder.ToTable("WAYPOINT_VISITS");

        builder.HasKey(wv => wv.Id);

        builder.Property(wv => wv.Id)
              .HasColumnType("uniqueidentifier")
              .HasDefaultValueSql("NEWID()")
              .HasColumnName("ID");

        builder.Property(wv => wv.StartTime)
               .IsRequired()
               .HasColumnName("START_TIME");

        builder.Property(wv => wv.EndTime)
               .IsRequired()
               .HasColumnName("END_TIME");

        builder.HasOne(wv => wv.Waypoint)
               .WithMany(w => w.WaypointVisits)
               .HasForeignKey(wv => wv.WaypointId)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();

        builder.HasOne(wv => wv.DayVisit)
               .WithMany(dv => dv.WaypointVisits)
               .HasForeignKey(wv => wv.DayVisitId)
               .OnDelete(DeleteBehavior.NoAction)
               .IsRequired();
    }
}
