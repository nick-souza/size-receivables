using Receivables.DTO.Outgoing;

namespace Receivables.Services.Companies;

public interface ICompanyService
{
    Task<List<CompanyDto>> GetCompanies();
    Task<CompanyDto> GetCompany(string cnpj);
    Task<CompanyDto> PostCompany(AddCompanyDto model);
    Task DeleteCompany(string cnpj);
    Task UpdateCompany(string cnpj, AddCompanyDto model);
    Task<ReceivableDto> CalculateReceivables(string cnpj, List<long> invoiceNumbers);
}