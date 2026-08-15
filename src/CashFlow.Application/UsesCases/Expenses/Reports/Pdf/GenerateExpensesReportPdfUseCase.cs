using CashFlow.Application.UsesCases.Expenses.Reports.Pdf.Colors;
using CashFlow.Application.UsesCases.Expenses.Reports.Pdf.Fonts;
using CashFlow.Domain.Entidades;
using CashFlow.Domain.Extensions;
using CashFlow.Domain.Reports;
using CashFlow.Domain.Repositories.Expenses;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Drawing;
using PdfSharp.Fonts;
using System.Net.WebSockets;
using System.Reflection;


namespace CashFlow.Application.UsesCases.Expenses.Reports.Pdf;

public class GenerateExpensesReportPdfUseCase : IGenerateExpensesReportPdfUseCase
{
    private const string CURRENCY_SYMBOL = "€";
    private const int HEIGHT_ROW_EXPENSES_TABLE = 25;

    private readonly IExpensesReadOnlyRepository _repository;



    public GenerateExpensesReportPdfUseCase(IExpensesReadOnlyRepository repository)
    {
        _repository = repository;

        GlobalFontSettings.FontResolver = new ExpensesReportFontResolver();
    }

    public async Task<byte[]> Execute(DateOnly monty)
    {
      var expenses = await _repository.FilterByMonth(monty);
      if(expenses.Count == 0)
      {
        return [];
      }
       var document = CreateDocument(monty);
       var page = CreatePage(document);
       

       createHeaherWithprofileePhotoAndName(page);

       var totalExpenses = expenses.Sum(expense => expense.Amount);
       CreateTotalSpentSection(page, monty, totalExpenses);

        foreach( var expense in expenses) 
        {
            var table = CreateExpenseTable(page);

            var row = table.AddRow();
            row.Height = HEIGHT_ROW_EXPENSES_TABLE;

            AddExpensesTitle(row.Cells[0],expense.Title);

            addHeaderForAmount(row.Cells[3]);

           
            row =  table.AddRow();
            row.Height = HEIGHT_ROW_EXPENSES_TABLE;

            row.Cells[0].AddParagraph(expense.Date.ToString("D"));
            SetStyleBaseForExpenseInformatio(row.Cells[0]);
            row.Cells[0].Format.LeftIndent = 20;

            row.Cells[1].AddParagraph(expense.Date.ToString("t"));
            SetStyleBaseForExpenseInformatio(row.Cells[1]);

            row.Cells[2].AddParagraph(expense.PaymentType.PymentTypeToString());
            SetStyleBaseForExpenseInformatio(row.Cells[2]);

            AddAmountForExpenses(row.Cells[3], expense.Amount);

            if (string.IsNullOrWhiteSpace(expense.Description) == false)
            {
                var descriptionRow = table.AddRow();
                descriptionRow.Height = HEIGHT_ROW_EXPENSES_TABLE;

               descriptionRow.Cells[0].AddParagraph(expense.Description);
               descriptionRow.Cells[0].Format.Font = new Font { Name = FontHelper.WORKSANS_REGULAR, Size = 10, Color = ColorsHelper.BLACK };
               descriptionRow.Cells[0].Shading.Color = ColorsHelper.GREEN_LIGHT;
               descriptionRow.Cells[0].VerticalAlignment = VerticalAlignment.Center;
               descriptionRow.Cells[0].MergeRight = 2;
               descriptionRow.Cells[0].Format.LeftIndent = 20;

               row.Cells[3].MergeDown = 1;
            }

           AddWhiteSpace(table);

        }
        

        return RenderDocument(document);
    }

    private Document CreateDocument(DateOnly month)
    {
        var document = new Document();
        document.Info.Title = $"{ResourceReportGenerationMessages.EXPENSES_FOR} {month:Y}";
        document.Info.Title = "MARK";

        var style = document.Styles["Normal"];
        style!.Font.Name = FontHelper.RALEWAY_REGULAR;
        return document;
    }

    private Section CreatePage(Document document)
    {
        var section = document.AddSection();
        section.PageSetup = document.DefaultPageSetup.Clone();

        section.PageSetup.PageFormat = PageFormat.A4;

        section.PageSetup.LeftMargin = 40;
        section.PageSetup.RightMargin = 40;
        section.PageSetup.TopMargin = 80;
        section.PageSetup.BottomMargin = 80;

        return section;
    }

    private void  createHeaherWithprofileePhotoAndName(Section page)
    {
    var table = page.AddTable();
    table.AddColumn();
    table.AddColumn("300");

    var row = table.AddRow();

    var assembly = Assembly.GetExecutingAssembly();
    var directoryname = Path.GetDirectoryName(assembly.Location);
    var pathFile = Path.Combine(directoryname!, "Logo", "logo_csharp_62x62.png");

    row.Cells[0].AddImage(pathFile);

    row.Cells[1].AddParagraph("Hey, Mark Ramos ");
    row.Cells[1].Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 16  };
    row.Cells[1].VerticalAlignment = MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Center;
    }

    private void CreateTotalSpentSection(Section page, DateOnly monty, decimal totalExpenses)
    {
    var paragraph = page.AddParagraph();
    paragraph.Format.SpaceBefore = "40";
    paragraph.Format.SpaceAfter = "40";

    var title = string.Format(ResourceReportGenerationMessages.TOTAL_SPENT_IN, monty.ToString("Y"));


    paragraph.AddFormattedText(title, new Font { Name = FontHelper.RALEWAY_REGULAR, Size = 15 });

    paragraph.AddLineBreak();

    paragraph.AddFormattedText($"{totalExpenses} {CURRENCY_SYMBOL}",new Font { Name = FontHelper.WORKSANS_BLACK, Size = 50 });

     
     }


    private  Table CreateExpenseTable(Section page)
    {
        var table =  page.AddTable();

        table.AddColumn("195").Format.Alignment = ParagraphAlignment.Left;
        table.AddColumn("80").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("120").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("120").Format.Alignment = ParagraphAlignment.Right;


        return table;
    }


    private void AddExpensesTitle(Cell  cell, string expenseTitle)
    {
     cell.AddParagraph(expenseTitle);
     cell.Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 14, Color = ColorsHelper.BLACK };
     cell.Shading.Color = ColorsHelper.RED_LIGHT;
     cell.VerticalAlignment = VerticalAlignment.Center;
     cell.MergeRight = 2;
     cell.Format.LeftIndent= 20;
    }

    private void addHeaderForAmount(Cell cell)
    { 
    cell.AddParagraph(ResourceReportGenerationMessages.AMOUNT);
    cell.Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 14, Color = ColorsHelper.WHITE };
    cell.Shading.Color = ColorsHelper.RED_DARK;
    cell.VerticalAlignment = VerticalAlignment.Center;
    }

    private void SetStyleBaseForExpenseInformatio(Cell cell)
    { 
     cell.Format.Font = new Font { Name = FontHelper.WORKSANS_REGULAR, Size = 12, Color = ColorsHelper.BLACK };
     cell.Shading.Color = ColorsHelper.GREEN_DARK;
     cell.VerticalAlignment = VerticalAlignment.Center;
     }

    private void AddAmountForExpenses(Cell cell,decimal amount)
    {
        cell.AddParagraph($"-{amount} {CURRENCY_SYMBOL}");
        cell.Format.Font = new Font { Name = FontHelper.WORKSANS_REGULAR, Size = 14, Color = ColorsHelper.BLACK };
        cell.Shading.Color = ColorsHelper.WHITE;
        cell.VerticalAlignment = VerticalAlignment.Center;
    }

    private void AddWhiteSpace(Table table)
    {
        var row = table.AddRow();
        row.Height = 30;
        row.Borders.Visible = false;
    }
    private byte[] RenderDocument(Document document)
    {
       var  render = new PdfDocumentRenderer

           {
            Document = document,
           };
           render.RenderDocument();

           using  var file = new MemoryStream();
           render.PdfDocument.Save(file);

           return file.ToArray();
    }
    

}
