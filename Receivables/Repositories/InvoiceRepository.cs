using Microsoft.EntityFrameworkCore;
using Receivables.Entities;

namespace Receivables.Repositories;

public class InvoiceRepository(AppDbContext context) : IInvoiceRepository
{
    public async Task<Invoice?> GetByIdAsync(long number) => await context.Invoice.FirstOrDefaultAsync(x => x.Number == number);

    public async Task<Invoice> AddAsync(Invoice invoice)
    {
        context.Invoice.Add(invoice);
        await context.SaveChangesAsync();

        return invoice;
    }

    public async Task<List<Invoice>> SelectByCompanyIdAsync(string cnpj) => await context.Invoice.Where(x => x.Cnpj == cnpj).ToListAsync();

    public async Task<List<Invoice>> SelectAsync() => await context.Invoice.ToListAsync();

    public async Task DeleteAsync(Invoice invoice)
    {
        context.Invoice.Remove(invoice);
        await context.SaveChangesAsync();
    }
}