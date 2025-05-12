using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Wanderer.Domain.Enums;
using Wanderer.Domain.Models.Locations;
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

        var converter = new ValueConverter<TripStatus, int>(
            x => (int)x,
            x => (TripStatus)x
        );

        builder.Property(t => t.Status)
               .IsRequired(true)
               .HasConversion(converter)
               .HasDefaultValue(TripStatus.NotCompleted);

        builder.HasIndex(x => x.OwnerId);

        builder.HasIndex(x => x.Status);
    }
}
