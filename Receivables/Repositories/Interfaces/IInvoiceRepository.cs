using Receivables.Entities;

namespace Receivables.Repositories;

public interface IInvoiceRepository
{
    Task<Invoice?> GetByIdAsync(long number);
    Task<Invoice> AddAsync(Invoice model);
    Task<List<Invoice>> SelectByCompanyIdAsync(string cnpj);
    Task<List<Invoice>> SelectAsync();
    Task DeleteAsync(Invoice invoice);
}