using Keycloak.API.Common;

namespace Keycloak.API.Application.Messages.Commands.User;

public record CreateUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password) : Command;

