using Receivables.Entities;

namespace Receivables.Repositories;

public interface ICompanyRepository
{
    Task<Company?> GetByIdAsync(string cnpj);
    Task<Company> AddAsync(Company company);
    Task<List<Company>> SelectAsync();
    Task UpdateAsync(Company model);
    Task DeleteAsync(Company company);
}