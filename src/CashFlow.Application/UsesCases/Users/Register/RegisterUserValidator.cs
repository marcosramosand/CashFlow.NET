using CashFlow.communication.Request;
using CashFlow.Domain.Entidades;
using CashFlow.Exception;
using FluentValidation;

namespace CashFlow.Application.UsesCases.Users.Register;

public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
{

    public RegisterUserValidator()
    { 
        RuleFor(User => User.Name).NotEmpty().WithMessage(ResourceErrorMessages.NAME_EMPTY);
        RuleFor(User => User.Email)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.EMAIL_EMPTY)
            .EmailAddress()
            .WithMessage(ResourceErrorMessages.EMAIL_INVALID);

        RuleFor(User => User.Password).SetValidator(new PasswordValidator<RequestRegisterUserJson>());

    }












}
