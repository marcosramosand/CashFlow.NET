namespace CashFlow.Domain.Security;

public interface IPassrowordEncripter
{
    string Encrypt(string password);
    bool Verify(string password, string passwordHash);

}
