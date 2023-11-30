using Keycloak.API.Authentication.Configuration;
using Keycloak.APIInfrastructure.Authentication.Models;
using Microsoft.Extensions.Options;

public class KeycloakDelegatingHandler : DelegatingHandler
{
    private readonly KeycloakConfigurations _keycloakConfigurations;

    public KeycloakDelegatingHandler(IOptions<KeycloakConfigurations> keycloakOptions)
    {
        _keycloakConfigurations = keycloakOptions.Value;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var authorizationToken = await GetAuthorizationTokenAsync(cancellationToken).ConfigureAwait(false);

        request.Headers.Add("Authorization", new List<string> { "Bearer " + authorizationToken.AccessToken });

        var httpResponseMessage = await base.SendAsync(request, cancellationToken);

        httpResponseMessage.EnsureSuccessStatusCode();

        return httpResponseMessage;
    }

    private async Task<AccessTokenModel> GetAuthorizationTokenAsync(CancellationToken cancellationToken)
    {
        var parameters = new Dictionary<string, string>
        {
            { "client_id", _keycloakConfigurations.ClientId },
            { "client_secret", _keycloakConfigurations.ClientSecret },
            { "scope", "openid email" },
            { "grant_type", "client_credentials" }
        };

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(_keycloakConfigurations.AuthUrl),
            Content = new FormUrlEncodedContent(parameters)
        };

        var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<AccessTokenModel>().ConfigureAwait(false);
    }
}
