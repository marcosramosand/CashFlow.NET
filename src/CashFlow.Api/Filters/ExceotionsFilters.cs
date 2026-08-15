using CashFlow.communication.Responses;
using CashFlow.Exception; 
using CashFlow.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CashFlow.Api.Filters;

public class ExceotionsFilters : IExceptionFilter
{
    

    public void OnException(ExceptionContext context)
    {
       if(context.Exception is CashFlowException)
        {
            HandleProjectExcertion(context);
        }
       else
        {
            ThrowUnkowError(context);
        }
    }


    private void HandleProjectExcertion(ExceptionContext context)

    {
        var CashFlowException =(CashFlowException) context.Exception ;
        var errorResponse = new ResponseErrorJson(CashFlowException.GetErrors());
        context.HttpContext.Response.StatusCode = CashFlowException.StausCode;
        context.Result = new ObjectResult(errorResponse);




       
    }
    private void ThrowUnkowError (ExceptionContext context)
    {
        var errorResponse = new ResponseErrorJson(CashFlow.Exception.ResourceErrorMessages.UNKNOWN_ERROR);

        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorResponse);

       
    }
}
