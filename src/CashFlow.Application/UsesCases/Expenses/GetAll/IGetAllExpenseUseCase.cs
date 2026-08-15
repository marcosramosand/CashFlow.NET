using CashFlow.communication.Request;
using CashFlow.communication.Responses;

namespace CashFlow.Application.UsesCases.Expenses.GetAll;

public interface IGetAllExpenseUseCase
{
    Task<ResponseExpensesJson> Execute();
}
