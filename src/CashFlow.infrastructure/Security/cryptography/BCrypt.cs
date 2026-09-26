using CashFlow.Domain.Security;
using BC = BCrypt.Net.BCrypt;

namespace CashFlow.infrastructure.Security.cryptography;

public class BCrypt : IPassrowordEncripter
{
    public string Encrypt(string password)
    {
        string passwordHash = BC.HashPassword(password);

        
        return passwordHash; 
    }

    public bool Verify(string password, string passwordHash)
    {
        return BC.Verify(password, passwordHash);
    }
}
