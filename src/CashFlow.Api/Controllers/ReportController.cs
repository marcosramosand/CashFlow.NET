using CashFlow.Application.UsesCases.Expenses.Reports.Excel;
using CashFlow.Application.UsesCases.Expenses.Reports.Pdf;
using CashFlow.communication.Request;
using CashFlow.communication.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace CashFlow.Api.Controllers;


[Route("api/[controller]")]
[ApiController]
public class ReportController : ControllerBase
{
    [HttpGet("Excel")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]

    public async Task<IActionResult> GetExcel(
        [FromServices] IGenerateExpensesReportExcelUseCase useCase,
        [FromHeader] DateOnly month)
    {
        byte[] file =  await useCase.Execute(month);


        if (file.Length > 0 )
            return File(file,MediaTypeNames.Application.Octet, "report.xlsx");

        return NoContent();
    }
    [HttpGet("pdf")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]

    public async Task<IActionResult> GetPdf(
        [FromServices] IGenerateExpensesReportPdfUseCase useCase,
        [FromHeader] DateOnly month)
    {
        byte[] file = await useCase.Execute(month);


        if (file.Length > 0)
            return File(file, MediaTypeNames.Application.Pdf, "report.pdf");

        return NoContent();
    }
}