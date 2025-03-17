using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Wanderer.Domain.Models.Locations;

namespace Wanderer.Infrastructure.Context.Configurations.Locations;

public class LatLngBoundConfiguration : IEntityTypeConfiguration<LatLngBound>
{
    public void Configure(EntityTypeBuilder<LatLngBound> builder)
    {
        builder.ToTable("LAT_LNG_BOUNDS");

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.CityId);

        builder.Property(x => x.Latitude)
            .HasColumnName("LATITUDE");

        builder.Property(x => x.Longitude)
            .HasColumnName("LONGITUDE");
    }
}
