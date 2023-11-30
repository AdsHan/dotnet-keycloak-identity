using Keycloak.API.Authentication.Interfaces;
using Keycloak.API.Common;
using MediatR;

namespace Keycloak.API.Application.Messages.Commands.Auth;

public class SignInCommandHandler : CommandHandler, IRequestHandler<SignInCommand, BaseResult>

{
    private readonly ITokenService _token;

    public SignInCommandHandler(ITokenService token)
    {
        _token = token;
    }

    public async Task<BaseResult> Handle(SignInCommand command, CancellationToken cancellationToken)
    {
        var result = await _token.GenerateTokenAsync(command.UserName, command.Password);

        if (string.IsNullOrWhiteSpace(result))
        {
            AddError("Não foi possível logar!");
            return BaseResult;
        }

        BaseResult.Response = result;

        return BaseResult;
    }
}
