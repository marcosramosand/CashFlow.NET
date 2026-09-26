using CashFlow.communication.Request;

namespace CashFlow.Application.UsesCases.Users.Register;

public interface IRegisterExpensesUseCase
{
     Task<RequestExpensesJson> Execute(RequestExpensesJson request);
}
