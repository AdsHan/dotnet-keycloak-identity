using Keycloak.API.Common;

namespace Keycloak.API.Authentication.Interfaces;

public interface IKeycloakTokenService
{
    Task<BaseResult> GenerateTokenAsync(string email, string password);
}