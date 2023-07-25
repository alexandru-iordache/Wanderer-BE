using Microsoft.EntityFrameworkCore;
using Wanderer.Domain.Models.Places;
using Wanderer.Domain.Models.Users;

namespace Wanderer.Infrastructure.Context;

public class WandererDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<City> Cities { get; set; }

    public DbSet<Country> Countries { get; set; }


    public WandererDbContext(DbContextOptions<WandererDbContext> options) : base(options)
    {
        this.Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }
}
