using AutoMapper;
using CashFlow.communication.Request;
using CashFlow.communication.Responses;
using CashFlow.Domain.Entidades;

namespace CashFlow.Application.AutoMapper;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToEntity();
        EntityToResponse();
    }
    //em CreateMap a origem dos dados e o destino dos dados//
    private void RequestToEntity()
    {
     CreateMap<RequestExpensesJson, Expense>();
    }
    private void EntityToResponse()
    {
     CreateMap <Expense,RequestExpensesJson>();
     CreateMap<Expense, ResponseShortExpensesJson>();
     CreateMap<Expense, ResponsesExpenseJson>();
    }

}
