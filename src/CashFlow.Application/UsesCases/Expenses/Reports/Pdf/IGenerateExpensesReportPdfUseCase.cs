namespace CashFlow.Application.UsesCases.Expenses.Reports.Pdf;
public interface IGenerateExpensesReportPdfUseCase
{
    Task<byte[]> Execute(DateOnly month);
}
