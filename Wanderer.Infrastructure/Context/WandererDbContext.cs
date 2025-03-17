using Microsoft.EntityFrameworkCore;
using Wanderer.Domain.Models.Locations;
using Wanderer.Domain.Models.Users;
using Wanderer.Infrastructure.Context.Configurations.Locations;
using Wanderer.Infrastructure.Context.Configurations.Places;
using Wanderer.Infrastructure.Context.Configurations.Users;

namespace Wanderer.Infrastructure.Context;

public class WandererDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<Country> Countries { get; set; }

    public DbSet<City> Cities { get; set; }

    public DbSet<Waypoint> Waypoints { get; set; }

    public DbSet<LatLngBound> Bounds { get; set; }

    public WandererDbContext(DbContextOptions<WandererDbContext> options) : base(options)
    {
    } 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new CountryConfiguration());
        modelBuilder.ApplyConfiguration(new CityConfiguration());
        modelBuilder.ApplyConfiguration(new WaypointConfiguration());
        modelBuilder.ApplyConfiguration(new LatLngBoundConfiguration());
    }
}
