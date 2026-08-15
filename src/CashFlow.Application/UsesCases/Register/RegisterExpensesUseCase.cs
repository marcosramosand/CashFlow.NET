using AutoMapper;
using CashFlow.Application.UsesCases.Expenses;
using CashFlow.communication.Request;
using CashFlow.Domain.Entidades;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionsBase;

using System.Data;

namespace CashFlow.Application.UsesCases.Register;

public class RegisterExpensesUseCase :  IRegisterExpensesUseCase
{

    private readonly IExpensesWriteOnlyRepository _reposotory;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public RegisterExpensesUseCase(
        IExpensesWriteOnlyRepository reposotory,
        IUnitOfWork unitOfWork,
        IMapper mapper)

    {
        _reposotory = reposotory;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    public async Task<RequestExpensesJson> Execute(RequestExpensesJson request)
    {
       validate(request);

        var entity = _mapper.Map<Expense>(request);

        await _reposotory.add(entity);

        await _unitOfWork.Comit();

        return _mapper.Map<RequestExpensesJson>(entity);
    }
    private void validate(RequestExpensesJson request)
    {
        var validator = new ExpensesValidation();

        var result = validator.Validate(request);

        if(result.IsValid== false)
        {
            var erroMessagens = result.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidationException(erroMessagens);
        }
    }
}

