namespace Keycloak.API.Authentication.Interfaces;

public interface ITokenService
{
    Task<string> GenerateTokenAsync(string email, string password);
}