using CashFlow.communication.Responses;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UsesCases.Expenses.Delete;

public class DeleteExpenseUseCase : IDeleteExpenseUseCase
{
    private readonly IExpensesWriteOnlyRepository _repositories;
    private readonly IUnitOfWork _unitofWork;
    public DeleteExpenseUseCase(
        IExpensesWriteOnlyRepository repositories,
        IUnitOfWork unitofWork)
    {
        _repositories = repositories;
        _unitofWork = unitofWork;
    }
    public async Task Execute(long id)
    {
        var result = await _repositories.Delete(id);
        
        if (result == false)
        {
            throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUND);
        }

        await _unitofWork.Comit();
    }

}
