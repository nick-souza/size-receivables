using Receivables.Entities;

namespace Receivables.Repositories;

public class CompanyRepository : ICompanyRepository
{
    public Task<Company> GetByIdAsync(int id) { throw new NotImplementedException(); }

    public Task<Company> AddAsync(Company company) { throw new NotImplementedException(); }

    public Task<IEnumerable<Company>> SelectAsync() { throw new NotImplementedException(); }

    public Task UpdateAsync(Company company) { throw new NotImplementedException(); }

    public Task DeleteAsync(int id) { throw new NotImplementedException(); }
}