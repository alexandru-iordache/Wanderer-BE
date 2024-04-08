using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Wanderer.API.Authentication;

namespace Wanderer.API;

public static class PresentationServices
{
    public static IServiceCollection AddPresentationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("MyAllowSpecificOrigins",
                builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .WithExposedHeaders("Authorization");
                });
        });

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = "Firebase";
            options.DefaultChallengeScheme = "Firebase";
        })
                .AddScheme<AuthenticationSchemeOptions, FirebaseAuthenticationHandler>("Firebase", options => { });

        services.AddAuthorization(options =>
        {
            options.DefaultPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .AddAuthenticationSchemes("Firebase")
                .Build();
        });

        return services;
    }
}
