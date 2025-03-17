using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using Wanderer.Domain.Models.Locations;

namespace Wanderer.Infrastructure.Context.Configurations.Places;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        var converter = new ValueConverter<LatLngBound, string>(
            x => JsonConvert.SerializeObject(x),
            x => JsonConvert.DeserializeObject<LatLngBound>(x)
        );

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

        builder.Property(t => t.NorthEastBound)
                .IsRequired(true)
                .HasConversion(converter)
                .HasColumnName("NORTH_EAST_BOUND");

        builder.Property(t => t.SouthWestBound)
               .IsRequired(true)
               .HasConversion(converter)
               .HasColumnName("SOUTH_WEST_BOUND");
    }
}
