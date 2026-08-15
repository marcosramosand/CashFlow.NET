using CashFlow.communication.Responses;

namespace CashFlow.Application.UsesCases.Expenses.Delete;

public interface IDeleteExpenseUseCase
{
    Task Execute(long id);
}
