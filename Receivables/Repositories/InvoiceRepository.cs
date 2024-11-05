using Receivables.Entities;

namespace Receivables.Repositories;

public class InvoiceRepository : IInvoiceRepository
{
    public Task<Invoice> GetByIdAsync(int id) { throw new NotImplementedException(); }

    public Task<Invoice> AddAsync(Invoice company) { throw new NotImplementedException(); }

    public Task<IEnumerable<Invoice>> SelectByCompanyIdAsync(Invoice company) { throw new NotImplementedException(); }

    public Task<IEnumerable<Invoice>> SelectAsync() { throw new NotImplementedException(); }

    public Task UpdateAsync(Invoice company) { throw new NotImplementedException(); }

    public Task DeleteAsync(int id) { throw new NotImplementedException(); }
}