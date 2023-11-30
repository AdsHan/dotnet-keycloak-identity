using FluentValidation;
using Keycloak.API.Application.InputModels;

namespace Keycloak.API.Application.Messages.Validators;

public class SignInValidator : AbstractValidator<SignInInputModel>
{
    public SignInValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage("O UserName não foi informado!");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("A Senha não foi informada!");
    }
}
