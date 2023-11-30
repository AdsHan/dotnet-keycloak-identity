using Keycloak.API.Authentication.Configuration;
using Keycloak.API.Authentication.Interfaces;
using Keycloak.API.Data.Entities;
using Keycloak.APIInfrastructure.Authentication.Models;
using Microsoft.Extensions.Options;

namespace Keycloak.API.Authentication.Services;

public class KeycloakService : IKeycloakService
{
    private readonly HttpClient _httpClient;

    public KeycloakService(HttpClient httpClient, IOptions<KeycloakConfigurations> keycloakConfigurations)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(keycloakConfigurations.Value.AccountUrl);
    }

    public async Task<string> CreateUserAsync(UserModel user, string password)
    {
        var userKeycloak = KeycloakUserModel.FromUser(user);

        userKeycloak.Credentials = new KeycloakCredentialModel[]
        {
            new()
            {
                Value = password,
                Temporary = false,
                Type = "password"
            }
        };

        var response = await _httpClient.PostAsJsonAsync("users", userKeycloak);

        return GetIdentityId(response);
    }

    private static string GetIdentityId(HttpResponseMessage response)
    {
        var locationHeader = response.Headers.Location?.PathAndQuery;

        var userSegmentValueIndex = locationHeader.IndexOf("users/", StringComparison.InvariantCultureIgnoreCase);

        return locationHeader.Substring(userSegmentValueIndex + 6);
    }
}