using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.Tasks;

namespace Wanderer.API.Config;

public static class AuthorizationConfigurationExtension
{
    public static IServiceCollection AddCustomAuthorization(this IServiceCollection services, IConfiguration configuration)
    {
        var issuerSection = configuration.GetSection("Jwt:Issuer") ?? throw new ArgumentException("Jwt:Issuer is missing in appsettings.json");
        var audienceSection = configuration.GetSection("Jwt:Audience") ?? throw new ArgumentException("Jwt:Audience is missing in appsettings.json");

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = issuerSection.Value;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = issuerSection.Value,
                        ValidateAudience = true,
                        ValidAudience = audienceSection.Value,
                        ValidateLifetime = true
                    };
                    options.RequireHttpsMetadata = false; // IMPORTANT remove this when productiom
                });

        services.AddAuthorization();

        return services;
    }
}
