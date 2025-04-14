using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wanderer.Domain.Enums;
using Wanderer.Domain.Models.Routes;

namespace Wanderer.Infrastructure.Context.Configurations.Routes;

public class RouteConfiguration : IEntityTypeConfiguration<Route>
{
    public void Configure(EntityTypeBuilder<Route> builder)
    {
        builder.ToTable("ROUTES");

        builder.HasKey(x => x.Id);

        builder.HasDiscriminator<RouteType>("ROUTE_TYPE")
               .HasValue<InterCityRoute>(RouteType.InterCityRoute)
               .HasValue<InterWaypointRoute>(RouteType.InterWaypointRoute);
    }
}
