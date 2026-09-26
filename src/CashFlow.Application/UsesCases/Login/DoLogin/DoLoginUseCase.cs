using CashFlow.communication.Request;
using CashFlow.communication.Responses;
using CashFlow.Domain.Repositories.User;
using CashFlow.Domain.Security;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UsesCases.Login.DoLogin;

public  class DoLoginUseCase : IDoLoginUseCase
{
    private readonly IUserReadOnlyRepository _repository;
    private readonly IPassrowordEncripter _passwordEncripter;
    private readonly IAccessTokenGenerator _AccessTokenGenerator;

    public DoLoginUseCase(
        IUserReadOnlyRepository repository,
        IPassrowordEncripter passwodEncripter,
        IAccessTokenGenerator accessTokenGenerator)
    {
        _repository = repository;
        _passwordEncripter = passwodEncripter;
        _AccessTokenGenerator = accessTokenGenerator;
    }

    public async Task<ResponseRegisterUserJson> Execute(RequestLoginJson request)
    {
        var user = await _repository.GetUserByEmail(request.Email);

        if (user == null) 
        {
            throw new  InvalidLoginExceptions();
        }

        var passwordMatch = _passwordEncripter.Verify(request.Password, user.Password);

        if (passwordMatch == false)
        {
            throw new InvalidLoginExceptions();
        }

        return new ResponseRegisterUserJson
        {
            Name = user.Name,
            Token = _AccessTokenGenerator.Generate(user)
        };
    }


}
