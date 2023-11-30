namespace Keycloak.API.Application.InputModels;

public record CreateUserInputModel(string FirstName, string LastName, string Email, string Password);
