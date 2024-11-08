using Microsoft.EntityFrameworkCore;
using Receivables.Entities;

namespace Receivables.Repositories;

public class CompanyRepository(AppDbContext context) : ICompanyRepository
{
    public async Task<Company?> GetByIdAsync(string cnpj) => await context.Company.Include(x => x.Invoices).FirstOrDefaultAsync(x => x.Cnpj == cnpj);

    public async Task<Company> AddAsync(Company company)
    {
        context.Company.Add(company);
        await context.SaveChangesAsync();

        return company;
    }

    public async Task<List<Company>> SelectAsync() => await context.Company.Include(x => x.Invoices).ToListAsync();

    public async Task UpdateAsync(Company company)
    {
        context.Company.Update(company);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Company company)
    {
        context.Invoice.RemoveRange(context.Invoice.Where(x => x.Cnpj == company.Cnpj));
        context.Company.Remove(company);

        await context.SaveChangesAsync();
    }
}