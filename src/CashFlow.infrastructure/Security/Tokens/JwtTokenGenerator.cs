using CashFlow.Domain.Entidades;
using CashFlow.Domain.Security.Tokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CashFlow.infrastructure.Security.Tokens;

internal class JwtTokenGenerator : IAccessTokenGenerator
{
    private readonly uint _expirationTimeMinutes;
    private readonly string _signinkey;

    public JwtTokenGenerator(uint expirationTimeMinutes, string signinkey)
    {
        _expirationTimeMinutes = expirationTimeMinutes;
        _signinkey = signinkey;
    }

    public string Generate(User user)
{
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Sid, user.UserIdentifier.ToString()),

        };
        var tokensDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddMinutes(_expirationTimeMinutes),
            SigningCredentials = new SigningCredentials(SecurityKey(), SecurityAlgorithms.HmacSha256Signature),
            Subject = new ClaimsIdentity(claims)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokensDescriptor);
        return tokenHandler.WriteToken(securityToken);
    }

    private SymmetricSecurityKey SecurityKey() 
    { 
        var key = Encoding.UTF8.GetBytes(_signinkey);

        return new SymmetricSecurityKey(key);
    }
}  