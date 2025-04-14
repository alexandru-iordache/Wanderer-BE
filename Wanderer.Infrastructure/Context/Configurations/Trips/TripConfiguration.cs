using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wanderer.Domain.Models.Trips;

namespace Wanderer.Infrastructure.Context.Configurations.Trips;

public class TripConfiguration : IEntityTypeConfiguration<Trip>
{
    public void Configure(EntityTypeBuilder<Trip> builder)
    {
        builder.ToTable("TRIPS");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
               .HasColumnType("uniqueidentifier")
               .HasDefaultValueSql("NEWID()")
               .HasColumnName("ID");

        builder.Property(t => t.Title)
               .IsRequired()
               .HasMaxLength(200)
               .HasColumnName("TITLE");

        builder.Property(t => t.StartDate)
               .IsRequired()
               .HasColumnName("START_DATE");

        builder.HasOne(t => t.Owner)
               .WithMany(u => u.Trips)
               .HasForeignKey(t => t.OwnerId);

        builder.HasMany(t => t.CityVisits)
               .WithOne(cv => cv.Trip)
               .HasForeignKey(cv => cv.TripId);
    }
}
