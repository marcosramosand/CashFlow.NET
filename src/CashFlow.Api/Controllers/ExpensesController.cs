using CashFlow.Application.UsesCases.Expenses.Delete;
using CashFlow.Application.UsesCases.Expenses.GetAll;
using CashFlow.Application.UsesCases.Expenses.GetById;
using CashFlow.Application.UsesCases.Expenses.Update;
using CashFlow.Application.UsesCases.Register;
using CashFlow.communication.Request;
using CashFlow.communication.Responses;
using CashFlow.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CashFlow.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExpensesController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponsesRegisterExpensesJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(

      [FromServices] IRegisterExpensesUseCase useCase,
      [FromBody] RequestExpensesJson request)
    {

        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseExpensesJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]

    public async Task<IActionResult> GetAllExpenses(
        [FromServices] IGetAllExpenseUseCase useCase)

    {
        var response = await useCase.Execute();
        if (response.Expenses.Count != 0)
            return Ok(response);

        return NoContent();
    }
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponsesExpenseJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(
        [FromServices] IGetExpenseByIdUseCase useCase,
        [FromRoute] long id)

    {
        var response = await useCase.Execute(id);

        return Ok(response);
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete(
        [FromServices] IDeleteExpenseUseCase useCase,
        [FromRoute] long id)

    {
        await useCase.Execute(id);

        return NoContent();

    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Update(
    [FromServices] IUpdateExpenseUseCase useCase,
    [FromRoute] long id,
    [FromBody] RequestExpensesJson request)
    {
        await useCase.Execute(id, request);

        return NoContent();
    }
}