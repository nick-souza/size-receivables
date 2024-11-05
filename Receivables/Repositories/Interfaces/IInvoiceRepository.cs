using Receivables.Entities;

namespace Receivables.Repositories;

public interface IInvoiceRepository
{
    Task<Invoice> GetByIdAsync(int id);
    Task<Invoice> AddAsync(Invoice company);
    Task<IEnumerable<Invoice>> SelectByCompanyIdAsync(Invoice company);
    Task<IEnumerable<Invoice>> SelectAsync();
    Task UpdateAsync(Invoice company);
    Task DeleteAsync(int id);
}