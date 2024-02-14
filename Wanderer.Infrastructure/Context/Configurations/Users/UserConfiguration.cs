using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wanderer.Domain.Models.Users;

namespace Wanderer.Infrastructure.Context.Configurations.Users;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("USERS");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnType("uniqueidentifier")
            .HasColumnName("ID");

        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(500)
            .HasColumnName("FIRST_NAME");

        builder.Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(500)
            .HasColumnName("LAST_NAME");

        builder.HasIndex(x => x.Email)
               .IsUnique();

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(500)
            .HasColumnName("EMAIL");

        builder.Property(x => x.Address)
            .HasMaxLength(1000)
            .HasColumnName("ADDRESS");
    }
}

