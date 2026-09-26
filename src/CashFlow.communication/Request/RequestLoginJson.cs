using System.Globalization;

namespace CashFlow.communication.Request;

public  class RequestLoginJson
{
    public String Email { get; set; } = string.Empty;

    public String Password { get; set; } = string.Empty;
}
