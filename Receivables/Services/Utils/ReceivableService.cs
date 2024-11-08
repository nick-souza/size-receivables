using API.Services.Errors;
using Receivables.DTO.Outgoing;
using Receivables.Entities;

namespace Receivables.Services.Utils;

public class ReceivableService(Company company, List<Invoice> invoices)
{
    private const double Rate = 0.0465;

    public ReceivableDto Calculate()
    {
        var creditLimit = CalculateCreditLimit();

        var totalCartValue = invoices.Sum(i => i.Value);
        if (totalCartValue > creditLimit) throw new BadRequestException("The total value of the selected invoices exceeds the credit limit.");

        var receivable = new ReceivableDto
        {
            company = company.Name,
            cnpj = company.Cnpj,
            limit = creditLimit,
            gross_total = Math.Round(totalCartValue, 2),
            net_total = invoices.Sum(CalculateLiquidValue),
            invoices = invoices.Select(invoice => new ReceivableInvoiceDto
            {
                number = invoice.Number,
                gross_total = Math.Round(invoice.Value, 2),
                net_total = CalculateLiquidValue(invoice)
            }).ToList(),
        };

        return receivable;
    }

    private double CalculateCreditLimit()
    {
        var limit = company.MonthlyBilling switch
        {
            >= 10001 and <= 50000 => company.MonthlyBilling * 0.5,

            >= 50001 and <= 100000 when company.Sector == Sector.Service => company.MonthlyBilling * 0.55,
            >= 50001 and <= 100000 when company.Sector == Sector.Product => company.MonthlyBilling * 0.6,

            > 100000 when company.Sector == Sector.Service => company.MonthlyBilling * 0.6,
            > 100000 when company.Sector == Sector.Product => company.MonthlyBilling * 0.65,

            _ => 0
        };

        return Math.Round(limit, 2);
    }

    private static double CalculateLiquidValue(Invoice invoice)
    {
        var daysUntilDue = (invoice.DueDate - DateTime.Now.Date).Days;

        var discountFactor = Math.Pow(1 + Rate, daysUntilDue / 30.0);
        var result = invoice.Value / discountFactor;

        return Math.Round(result, 2);
    }
}