namespace Keycloak.API.Authentication.Configuration;

public class AuthConfigurations
{
    public string Audience { get; set; }
    public string MetadataUrl { get; set; }
    public bool RequireHttpsMetadata { get; set; }
    public string ValidIssuer { get; set; }
}