using CashFlow.communication.Request;

namespace CashFlow.Application.UsesCases.Register;

public interface IRegisterExpensesUseCase
{
     Task<RequestExpensesJson> Execute(RequestExpensesJson request);
}
