using CashFlow.Domain.Enums;
using CashFlow.Domain.Reports;
using CashFlow.Exception;

namespace CashFlow.Domain.Extensions;

public static class PymentTypeExtension
{
    public static string PymentTypeToString(this PaymentType paymentType)
    {
        return paymentType switch
        {
           PaymentType.cash => ResourceReportGenerationMessages.CASH,
           PaymentType.CrediCard => ResourceReportGenerationMessages.CREDI_CARD,
           PaymentType.DebitCard => ResourceReportGenerationMessages.DEBIT_CARD,
           PaymentType.eletronicTransfer => ResourceReportGenerationMessages.ELETRONIC_TRANSFER,
            _ => string.Empty
        };

    }
}
