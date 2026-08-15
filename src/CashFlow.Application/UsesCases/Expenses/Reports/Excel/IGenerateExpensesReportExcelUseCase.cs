namespace CashFlow.Application.UsesCases.Expenses.Reports.Excel;

public  interface IGenerateExpensesReportExcelUseCase
{
    Task<byte[]> Execute(DateOnly month);
}
