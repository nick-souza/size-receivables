using Receivables.Entities;

namespace Receivables.Repositories;

public interface ICompanyRepository
{
    Task<Company> GetByIdAsync(int id);
    Task<Company> AddAsync(Company company);
    Task<IEnumerable<Company>> SelectAsync();
    Task UpdateAsync(Company company);
    Task DeleteAsync(int id);
}