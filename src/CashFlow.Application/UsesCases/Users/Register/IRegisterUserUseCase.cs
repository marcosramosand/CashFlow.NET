using CashFlow.communication.Request;
using CashFlow.communication.Responses;

namespace CashFlow.Application.UsesCases.Users.Register;

public interface IRegisterUserUseCase
{
    Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson request);
}
