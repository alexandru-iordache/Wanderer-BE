using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wanderer.Domain.Models.Locations;

namespace Wanderer.Infrastructure.Context.Configurations.Places;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.ToTable("CITIES");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnType("uniqueidentifier")
            .HasDefaultValueSql("NEWID()")
            .HasColumnName("ID");

        builder.Property(x => x.PlaceId)
            .HasColumnName("PLACE_ID");

        builder.HasIndex(x => x.PlaceId)
            .IsUnique();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnName("NAME");

        builder.Property(x => x.Description)
            .HasColumnName("DESCRIPTION");

        builder.HasOne(x => x.Country)
            .WithMany(c => c.Cities)
            .HasForeignKey(x => x.CountryId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasOne(x => x.NorthEastBound)
            .WithOne()
            .HasForeignKey<City>(x => x.NorthEastBoundId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasOne(x => x.SouthWestBound)
            .WithOne()
            .HasForeignKey<City>(x => x.SouthWestBoundId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}
