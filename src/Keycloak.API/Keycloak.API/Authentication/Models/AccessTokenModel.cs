using System.Text.Json.Serialization;

namespace Keycloak.APIInfrastructure.Authentication.Models;

public class AccessTokenModel
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }
}