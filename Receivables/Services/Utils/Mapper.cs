using Receivables.DTO.Outgoing;
using Receivables.Entities;

namespace Receivables.Services.Utils;

public abstract class Mapper
{
    public static InvoiceDto MapToInvoiceDto(Invoice invoice) => new()
    {
        number = invoice.Number,
        cnpj = invoice.Cnpj,
        value = invoice.Value,
        dueDate = invoice.DueDate.ToString("dd/MM/yyyy")
    };

    public static CompanyDto MapToCompanyDto(Company company) => new()
    {
        cnpj = company.Cnpj,
        name = company.Name,
        sector = company.Sector.ToString(),
        monthlyBilling = company.MonthlyBilling,
        invoices = company.Invoices.Select(MapToInvoiceDto).ToList()
    };
}