using Keycloak.API.Authentication.Interfaces;
using Keycloak.API.Common;
using Keycloak.API.Data;
using Keycloak.API.Data.Entities;
using MediatR;

namespace Keycloak.API.Application.Messages.Commands.User;

public class UserCommandHandler : CommandHandler, IRequestHandler<CreateUserCommand, BaseResult>

{
    private readonly KeycloakDbContext _dbContext;
    private readonly IKeycloakService _keycloak;

    public UserCommandHandler(KeycloakDbContext dbContext, IKeycloakService keycloak)
    {
        _dbContext = dbContext;
        _keycloak = keycloak;
    }

    public async Task<BaseResult> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var user = new UserModel()
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email,
            Password = command.Password
        };

        var identityId = await _keycloak.CreateUserAsync(user, command.Password);

        user.IdentityId = identityId;

        _dbContext.Add(user);

        await _dbContext.SaveChangesAsync();

        BaseResult.Response = user.Id;

        return BaseResult;
    }
}
