using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wanderer.Domain.Models.Trips.Visits;

namespace Wanderer.Infrastructure.Context.Configurations.Trips.Visits;

public class CityVisitConfiguration : IEntityTypeConfiguration<CityVisit>
{
    public void Configure(EntityTypeBuilder<CityVisit> builder)
    {
        builder.ToTable("CITY_VISITS");

        builder.HasKey(cv => cv.Id);

        builder.Property(cv => cv.Id)
               .HasColumnType("uniqueidentifier")
               .HasDefaultValueSql("NEWID()")
               .HasColumnName("ID");

        builder.Property(cv => cv.StartDate)
               .IsRequired()
               .HasColumnName("START_DATE");

        builder.Property(cv => cv.NumberOfNights)
               .IsRequired()
               .HasColumnName("NO_OF_NIGHTS");

        builder.Property(cv => cv.Order)
               .IsRequired()
               .HasColumnName("ORDER");

        builder.HasOne(cv => cv.City)
               .WithMany(c => c.CityVisits)
               .HasForeignKey(cv => cv.CityId)
               .IsRequired();

        builder.HasOne(cv => cv.Trip)
               .WithMany(t => t.CityVisits)
               .HasForeignKey(cv => cv.TripId)
               .IsRequired();
    }
}
