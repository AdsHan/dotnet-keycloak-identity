using Keycloak.API.Data.Entities;

namespace Keycloak.API.Authentication.Interfaces;

public interface IKeycloakService
{
    Task<string> CreateUserAsync(UserModel user, string password);
}