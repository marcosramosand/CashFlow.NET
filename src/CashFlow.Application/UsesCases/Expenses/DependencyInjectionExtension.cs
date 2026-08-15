using CashFlow.Application.AutoMapper;
using CashFlow.Application.UsesCases.Expenses.Delete;
using CashFlow.Application.UsesCases.Expenses.GetAll;
using CashFlow.Application.UsesCases.Expenses.GetById;
using CashFlow.Application.UsesCases.Expenses.Reports.Excel;
using CashFlow.Application.UsesCases.Expenses.Reports.Pdf;
using CashFlow.Application.UsesCases.Expenses.Update;
using CashFlow.Application.UsesCases.Register;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Application;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        addAutoMapper(services);
        addUseCAses(services);
    }
    private static void addAutoMapper(IServiceCollection services)
    {
        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<AutoMapping>();
        });
    }
    private static void addUseCAses(IServiceCollection services)
    {
        services.AddScoped<IRegisterExpensesUseCase, RegisterExpensesUseCase>();
        services.AddScoped<IGetAllExpenseUseCase, GetAllExpenseUseCase>();
        services.AddScoped<IGetExpenseByIdUseCase, GetExpensesByIdUserCase>();
        services.AddScoped<IDeleteExpenseUseCase, DeleteExpenseUseCase>();
        services.AddScoped<IUpdateExpenseUseCase, UpdateExpenseUseCase>();
        services.AddScoped<IGenerateExpensesReportExcelUseCase, GenerateExpensesReportExcelUseCase>(); 
         services.AddScoped<IGenerateExpensesReportPdfUseCase, GenerateExpensesReportPdfUseCase>();
    }
}