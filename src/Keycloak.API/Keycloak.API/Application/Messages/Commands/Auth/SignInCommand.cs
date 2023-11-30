using Keycloak.API.Common;

namespace Keycloak.API.Application.Messages.Commands.Auth;

public record SignInCommand(string UserName, string Password) : Command;

