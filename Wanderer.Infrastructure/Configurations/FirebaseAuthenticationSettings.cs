namespace Wanderer.Infrastructure.Configurations;

public class FirebaseAuthenticationSettings
{
    public bool ValidateIssuer { get; set; }
    public string ValidIssuer { get; set; }
    public bool ValidateAudience { get; set; }
    public string ValidAudience { get; set; }
    public bool ValidateLifetime { get; set; }
}
