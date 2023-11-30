using FluentValidation;
using Keycloak.API.Application.InputModels;

namespace Keycloak.API.Application.Messages.Validators;

public class CreateUserValidator : AbstractValidator<CreateUserInputModel>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("O Nome não foi informado!");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("O Sobrenome não foi informado!");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("O E-mail não foi informado!")
            .EmailAddress().WithMessage("O E-mail inválido!");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("A Senha não foi informada!")
            .MinimumLength(3).WithMessage("A Senha deve conter ao menos 3 caracteres");
    }
}
