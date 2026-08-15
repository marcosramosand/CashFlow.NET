using AutoMapper;
using CashFlow.communication.Responses;
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Application.UsesCases.Expenses.GetAll;

public class GetAllExpenseUseCase : IGetAllExpenseUseCase

{
    private readonly IExpensesReadOnlyRepository _repositories;
    private readonly IMapper _mapper;
    public GetAllExpenseUseCase(IExpensesReadOnlyRepository repositories, IMapper mapper)
    {
        _repositories = repositories;
        _mapper = mapper;
    }
    public async Task<ResponseExpensesJson> Execute()
    {
        var result = await _repositories.GetAll();
        return new ResponseExpensesJson
        {
            Expenses = _mapper.Map<List<ResponseShortExpensesJson>>(result)
        };
    }
}
