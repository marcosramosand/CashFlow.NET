using CashFlow.Domain.Entidades;

namespace CashFlow.Domain.Repositories.Expenses;

public interface IExpensesWriteOnlyRepository
{
    Task add(Expense expense);
    /// <summary>
    /// This function returns TRUE if the delection was sucessful otherwise returns false
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task <bool> Delete(long id);
}
