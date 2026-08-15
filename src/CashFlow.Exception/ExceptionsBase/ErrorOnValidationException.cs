using System.Net;

namespace CashFlow.Exception.ExceptionsBase;

public  class ErrorOnValidationException : CashFlowException

{
    private  readonly List<string> _errors ; 

    public override int StausCode =>(int)HttpStatusCode.BadRequest;

    public ErrorOnValidationException(List<string> erroMessages) : base(string.Empty)
    {
        _errors = erroMessages;

    }

    public override List<string> GetErrors()
    {
        return _errors;
    }
}
