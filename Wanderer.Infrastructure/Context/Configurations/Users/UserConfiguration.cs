using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wanderer.Domain.Models.Users;
using Wanderer.Shared.Constants;

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

        builder.Property(x => x.ExternalId)
            .HasColumnName("EXTERNAL_ID")
            .IsRequired();

        builder.HasIndex(x => x.ExternalId)
            .IsUnique();

        builder.Property(x => x.ProfileName)
            .HasMaxLength(DBConstants.PROFILE_NAME_MAX_LENGTH)
            .HasColumnName("PROFILE_NAME")
            .IsRequired();
            
        builder.HasIndex(x => x.ProfileName)
            .IsUnique();

        builder.Property(x => x.FirstName)
            .HasMaxLength(DBConstants.MAX_LENGTH)
            .HasColumnName("FIRST_NAME");

        builder.Property(x => x.LastName)
            .HasMaxLength(DBConstants.MAX_LENGTH)
            .HasColumnName("LAST_NAME");

        builder.HasIndex(x => x.Email)
               .IsUnique();

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(DBConstants.MAX_LENGTH)
            .HasColumnName("EMAIL");

        builder.Property(x => x.Address)
            .HasMaxLength(DBConstants.ADDRESS_MAX_LENGTH)
            .HasColumnName("ADDRESS");

        builder.Property(x => x.BirthDate)
            .HasColumnName("BIRTH_DATE");
    }
}

