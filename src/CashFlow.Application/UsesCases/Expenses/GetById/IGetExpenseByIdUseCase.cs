using CashFlow.communication.Request;
using CashFlow.communication.Responses;

namespace CashFlow.Application.UsesCases.Expenses.GetById;

public interface IGetExpenseByIdUseCase
{
    Task<ResponsesExpenseJson> Execute(long id);
}
