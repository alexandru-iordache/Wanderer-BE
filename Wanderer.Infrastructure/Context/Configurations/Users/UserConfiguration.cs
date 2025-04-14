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
            .HasDefaultValueSql("NEWID()")
            .HasColumnType("uniqueidentifier")
            .HasColumnName("ID");

        builder.Property(x => x.ProfileName)
            .IsRequired()
            .HasMaxLength(500)
            .HasColumnName("PROFILE_NAME");

        builder.Property(x => x.FirebaseId)
            .IsRequired()
            .HasMaxLength(500)
            .HasColumnName("FIREBASE_ID");

        builder.HasIndex(x => x.FirebaseId)
            .IsUnique();

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

