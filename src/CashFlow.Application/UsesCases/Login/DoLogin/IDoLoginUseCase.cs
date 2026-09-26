using CashFlow.communication.Request;
using CashFlow.communication.Responses;

namespace CashFlow.Application.UsesCases.Login.DoLogin;

public  interface IDoLoginUseCase
{
    Task<ResponseRegisterUserJson> Execute(RequestLoginJson request);
}
