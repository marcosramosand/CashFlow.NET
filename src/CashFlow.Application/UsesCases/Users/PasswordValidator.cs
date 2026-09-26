using CashFlow.Exception;
using FluentValidation;
using FluentValidation.Validators;
using System.Text.RegularExpressions;

namespace CashFlow.Application.UsesCases.Users;

public partial class PasswordValidator<T> : PropertyValidator<T, string>
{
    private const string ERROR_MESSAGE = "ErrorMessage";



    public override string Name => "PasswordValidator";

    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return $"{{{ERROR_MESSAGE}}}";
    }

    public override bool IsValid(ValidationContext<T> context, string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE, ResourceErrorMessages.INVALID_PASSWORD);
            return false;
        }
        if (password.Length < 8)
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE, ResourceErrorMessages.INVALID_PASSWORD);
            return false;
        }

        if (UpperCaseLetter().IsMatch(password) == false)
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE, ResourceErrorMessages.INVALID_PASSWORD);
            return false;
        }
        if (LowercaseLetter().IsMatch(password) == false)
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE, ResourceErrorMessages.INVALID_PASSWORD);
            return false;
        }
        if (Numbers().IsMatch(password) == false)
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE, ResourceErrorMessages.INVALID_PASSWORD);
            return false;
        }
        if (SpecialSymbols().IsMatch(password) == false)
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE, ResourceErrorMessages.INVALID_PASSWORD);
            return false;
        }

        return true;
    }

    [GeneratedRegex(@"[A-Z]+")]
    private static partial Regex UpperCaseLetter();
    [GeneratedRegex(@"[a-z]+")]
    private static partial Regex LowercaseLetter();
    [GeneratedRegex(@"[0-9]+")]
    private static partial Regex Numbers();
    [GeneratedRegex(@"[\?\!\*\.]+")]
    private static partial Regex SpecialSymbols();
}
