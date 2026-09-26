using System.Net;

namespace CashFlow.Exception.ExceptionsBase;

public class InvalidLoginExceptions : CashFlowException
{
    public InvalidLoginExceptions() : base(ResourceErrorMessages.EMAIL_OR_PASSWORD_INVALID)
    {
    }
    public override int StausCode => (int)HttpStatusCode.Unauthorized;

    public override List<string> GetErrors()
    {
        return [Message];
    }
}