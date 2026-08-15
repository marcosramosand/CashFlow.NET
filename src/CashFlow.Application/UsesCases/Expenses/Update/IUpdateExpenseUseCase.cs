using CashFlow.communication.Request;
using CashFlow.communication.Responses;

namespace CashFlow.Application.UsesCases.Expenses.Update;

public interface IUpdateExpenseUseCase
{
    Task Execute(long id, RequestExpensesJson request);
}
