using Microsoft.AspNetCore.Mvc;
using Receivables.DTO.Outgoing;
using Receivables.Services.Companies;

namespace Receivables.Controllers;

[ApiController]
[Route("[controller]")]
public class CompanyController(ICompanyService service) : BaseController
{
    [HttpGet]
    public async Task<ActionResult<Response<List<CompanyDto>>>> GetCompanies()
    {
        var result = await service.GetCompanies();
        return OkResponse(result);
    }

    [HttpGet("{cnpj}")]
    public async Task<ActionResult<Response<CompanyDto>>> GetCompany(string cnpj)
    {
        var result = await service.GetCompany(cnpj);
        return OkResponse(result);
    }

    [HttpPost]
    public async Task<ActionResult<Response<CompanyDto>>> PostCompany([FromBody] AddCompanyDto company)
    {
        var result = await service.PostCompany(company);
        return OkResponse(result);
    }

    [HttpDelete("{cnpj}")]
    public async Task<ActionResult<Response>> DeleteCompany(string cnpj)
    {
        await service.DeleteCompany(cnpj);
        return OkResponse();
    }

    [HttpPut("{cnpj}")]
    public async Task<ActionResult<Response>> UpdateCompany([FromRoute] string cnpj, [FromBody] AddCompanyDto company)
    {
        await service.UpdateCompany(cnpj, company);
        return OkResponse();
    }

    [HttpPost("{cnpj}/checkout")]
    public async Task<ActionResult<Response<ReceivableDto>>> CalculateReceivables(string cnpj, [FromBody] List<long> invoiceNumbers)
    {
        var result = await service.CalculateReceivables(cnpj, invoiceNumbers);
        return OkResponse(result);
    }
}