using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wanderer.Domain.Models.Trips.Visits;

namespace Wanderer.Infrastructure.Context.Configurations.Trips.Visits;

public class DayVisitConfiguration : IEntityTypeConfiguration<DayVisit>
{
    public void Configure(EntityTypeBuilder<DayVisit> builder)
    {
        builder.ToTable("DAY_VISITS");

        builder.HasKey(dv => dv.Id);

        builder.Property(dv => dv.Id)
               .HasColumnType("uniqueidentifier")
               .HasDefaultValueSql("NEWID()")
               .HasColumnName("ID");

        builder.Property(dv => dv.Date)
               .IsRequired()
               .HasColumnName("DATE");

        builder.HasOne(dv => dv.CityVisit)
               .WithMany(cv => cv.Days)
               .HasForeignKey(dv => dv.CityVisitId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
