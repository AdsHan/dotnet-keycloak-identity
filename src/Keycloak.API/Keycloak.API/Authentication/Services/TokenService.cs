using Keycloak.API.Authentication.Configuration;
using Keycloak.API.Authentication.Interfaces;
using Keycloak.APIInfrastructure.Authentication.Models;
using Microsoft.Extensions.Options;
using System.Net;

namespace Keycloak.API.Authentication.Services;

public class TokenService : ITokenService
{
    private readonly HttpClient _httpClient;
    private readonly KeycloakConfigurations _keycloakConfigurations;

    public TokenService(HttpClient httpClient, IOptions<KeycloakConfigurations> keycloakConfigurations)
    {
        _httpClient = httpClient;
        _keycloakConfigurations = keycloakConfigurations.Value;
        _httpClient.BaseAddress = new Uri(keycloakConfigurations.Value.AuthUrl);
    }

    public async Task<string> GenerateTokenAsync(string email, string password)
    {
        var parameters = new KeyValuePair<string, string>[]
        {
            new("client_id", _keycloakConfigurations.ClientId),
            new("client_secret", _keycloakConfigurations.ClientSecret),
            new("scope", "openid email"),
            new("grant_type", "password"),
            new("username", email),
            new("password", password)
        };

        var content = new FormUrlEncodedContent(parameters);

        var response = await _httpClient.PostAsync("", content);

        if (response.StatusCode != HttpStatusCode.OK)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();

        var token = await response.Content.ReadFromJsonAsync<AccessTokenModel>();

        return token.AccessToken;
    }
}