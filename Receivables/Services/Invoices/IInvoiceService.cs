using Receivables.DTO.Outgoing;

namespace Receivables.Services.Invoices;

public interface IInvoiceService
{
    Task<List<InvoiceDto>> GetInvoices();
    Task<InvoiceDto> GetInvoice(long number);
    Task<List<InvoiceDto>> GetInvoicesByCompany(string cnpj);
    Task<InvoiceDto> PostInvoice(AddInvoiceDto model);
    Task DeleteInvoice(long number);
}