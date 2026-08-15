using AutoMapper;
using CashFlow.communication.Responses;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UsesCases.Expenses.GetById;

public class GetExpensesByIdUserCase : IGetExpenseByIdUseCase
{
    private readonly IExpensesReadOnlyRepository _repository;
    private readonly IMapper _mapper;


    public GetExpensesByIdUserCase(IExpensesReadOnlyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<ResponsesExpenseJson> Execute(long id)
    {
        var result = await _repository.GetById(id);

        if (result is null)

        {
            throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUND);
        }

        return _mapper.Map<ResponsesExpenseJson>(result);
    }

}