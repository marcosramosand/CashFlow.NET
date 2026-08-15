using CashFlow.Domain.Extensions;
using CashFlow.Domain.Reports;
using CashFlow.Domain.Repositories.Expenses;
using ClosedXML.Excel;

namespace CashFlow.Application.UsesCases.Expenses.Reports.Excel;



public class GenerateExpensesReportExcelUseCase : IGenerateExpensesReportExcelUseCase
{
   private const string CURRENCY_SYMBOL = "€";
    private  readonly IExpensesReadOnlyRepository _repository;

    public GenerateExpensesReportExcelUseCase (IExpensesReadOnlyRepository repository)
    {
        _repository = repository;
    }

    public async Task<byte[]> Execute(DateOnly month)
  {

       var expenses = await _repository.FilterByMonth(month);
       if(expenses.Count == 0)
       {
            return [];
       }

       using var Workbook =  new XLWorkbook();

       Workbook.Author = "Mark";
       Workbook.Style.Font.FontSize = 12;
       Workbook.Style.Font.FontName = "Times New Roman";

       var Worksheet = Workbook.Worksheets.Add(month.ToString("Y"));

       InsertHeader(Worksheet);

       var raw = 2;
        foreach ( var expense in expenses) 
        {
         Worksheet.Cell($"A{raw}").Value = expense.Title;
         Worksheet.Cell($"B{raw}").Value = expense.Date;
         Worksheet.Cell($"C{raw}").Value = expense.PaymentType.PymentTypeToString();

         Worksheet.Cell($"D{raw}").Value = expense.Amount;
         Worksheet.Cell($"D{raw}").Style.NumberFormat.Format = $"-{CURRENCY_SYMBOL} #,##0.00";

         Worksheet.Cell($"E{raw}").Value = expense.Description;
            raw++;
        }

        Worksheet .Columns().AdjustToContents();

        var file = new MemoryStream();
        Workbook.SaveAs(file);

        return file.ToArray();
        
    }


    private void InsertHeader(IXLWorksheet worksheet)
   {
     worksheet.Cell("A1").Value = ResourceReportGenerationMessages.TITLE;
     worksheet.Cell("B1").Value = ResourceReportGenerationMessages.DATE;
     worksheet.Cell("C1").Value = ResourceReportGenerationMessages.PAYMENT_TYPE; 
     worksheet.Cell("D1").Value = ResourceReportGenerationMessages.AMOUNT;
     worksheet.Cell("E1").Value = ResourceReportGenerationMessages.DESCRIPTION;

     worksheet.Cells("A1:E1").Style.Font.Bold = true;

     worksheet.Cells("A1:E1").Style.Fill.BackgroundColor = XLColor.FromHtml("#F5C2B6");

     worksheet.Cell("A1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
     worksheet.Cell("B1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
     worksheet.Cell("C1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
     worksheet.Cell("E1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
     worksheet.Cell("D1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
    }
}