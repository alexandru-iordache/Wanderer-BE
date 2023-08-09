using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wanderer.Domain.Models.Places;

namespace Wanderer.Infrastructure.Context.Configurations.Places;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.ToTable("CITIES");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnType("uniqueidentifier")
            .HasColumnName("ID");

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnName("NAME");

        builder.HasOne(x => x.Country)
            .WithMany(c => c.Cities)
            .HasForeignKey(x => x.CountryId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}
