using API.Services.Errors;
using Receivables.DTO.Outgoing;
using Receivables.Entities;
using Receivables.Repositories;
using Receivables.Services.Utils;

namespace Receivables.Services.Companies;

public class CompanyService(ICompanyRepository repository) : ICompanyService
{
    public async Task<List<CompanyDto>> GetCompanies()
    {
        var result = await repository.SelectAsync();
        return result.Select(Mapper.MapToCompanyDto).ToList();
    }

    public async Task<CompanyDto> GetCompany(string cnpj)
    {
        if (string.IsNullOrWhiteSpace(cnpj)) throw new BadRequestException("Invalid CNPJ");

        var result = await repository.GetByIdAsync(cnpj);
        if (result == null) throw new RecordNotFoundException("Company not found");

        return Mapper.MapToCompanyDto(result);
    }

    public async Task<CompanyDto> PostCompany(AddCompanyDto model)
    {
        if (!ValidateCnpj(model.cnpj)) throw new BadRequestException("Invalid CNPJ");

        var alreadyExists = await ExistsAsync(model.cnpj);
        if (alreadyExists) throw new BadRequestException("Company already exists");

        if (model.sector == null) throw new BadRequestException("Sector is required");
        if (model.monthlyBilling == null) throw new BadRequestException("Monthly billing is required");

        var company = new Company
        {
            Cnpj = model.cnpj,
            Name = model.name,
            Sector = model.sector.Value,
            MonthlyBilling = model.monthlyBilling.Value,
        };

        var result = await repository.AddAsync(company);
        return Mapper.MapToCompanyDto(result);
    }

    public async Task DeleteCompany(string cnpj)
    {
        if (string.IsNullOrWhiteSpace(cnpj)) throw new BadRequestException("Invalid CNPJ");

        var company = await repository.GetByIdAsync(cnpj);
        if (company == null) throw new RecordNotFoundException("Company not found");

        await repository.DeleteAsync(company);
    }

    public async Task UpdateCompany(string cnpj, AddCompanyDto model)
    {
        if (string.IsNullOrWhiteSpace(cnpj)) throw new BadRequestException("Invalid CNPJ");
        if (cnpj != model.cnpj) throw new BadRequestException("CNPJs do not match");

        var company = await repository.GetByIdAsync(cnpj);
        if (company == null) throw new RecordNotFoundException("Company not found");

        if (!string.IsNullOrWhiteSpace(model.name)) company.Name = model.name;
        if (model.sector.HasValue) company.Sector = model.sector.Value;
        if (model.monthlyBilling.HasValue) company.MonthlyBilling = model.monthlyBilling.Value;

        await repository.UpdateAsync(company);
    }

    public async Task<ReceivableDto> CalculateReceivables(string cnpj, List<long> invoiceNumbers)
    {
        if (string.IsNullOrWhiteSpace(cnpj)) throw new BadRequestException("Invalid CNPJ");

        var company = await repository.GetByIdAsync(cnpj);
        if (company == null) throw new RecordNotFoundException("Company not found");

        var invoices = company.Invoices.Where(x => invoiceNumbers.Contains(x.Number)).ToList();
        if (invoices.Count == 0) throw new RecordNotFoundException("No invoices found");
        if (invoices.Count != invoiceNumbers.Count) throw new BadRequestException("Some invoices were not found");

        ValidateInvoicesDate(invoices);

        var service = new ReceivableService(company, invoices);
        var receivable = service.Calculate();

        return receivable;
    }

    private async Task<bool> ExistsAsync(string cnpj)
    {
        var company = await repository.GetByIdAsync(cnpj);
        return company != null;
    }

    private static bool ValidateCnpj(string cnpj) => cnpj.Length == 14 && cnpj.All(char.IsDigit);

    private static void ValidateInvoicesDate(List<Invoice> invoices)
    {
        var today = DateTime.Now.Date;
        var invalidInvoices = invoices.Where(x => x.DueDate < today).ToList();
        if (invalidInvoices.Count > 0) throw new BadRequestException("Some invoices are overdue");
    }
}