using Microsoft.EntityFrameworkCore;
using Wanderer.Domain.Models.Places;
using Wanderer.Domain.Models.Users;
using Wanderer.Infrastructure.Context.Configurations.Places;
using Wanderer.Infrastructure.Context.Configurations.Users;

namespace Wanderer.Infrastructure.Context;

public class WandererDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<Country> Countries { get; set; }

    public DbSet<City> Cities { get; set; }

    public WandererDbContext(DbContextOptions<WandererDbContext> options) : base(options)
    {
    } 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new CountryConfiguration());
        modelBuilder.ApplyConfiguration(new CityConfiguration());
    }
}
