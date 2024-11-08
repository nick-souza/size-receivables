using Microsoft.AspNetCore.Mvc;
using Receivables.DTO.Outgoing;
using Receivables.Services.Invoices;

namespace Receivables.Controllers;

[ApiController]
[Route("[controller]")]
public class InvoiceController(IInvoiceService service) : BaseController
{
    [HttpGet]
    public async Task<ActionResult<Response<List<InvoiceDto>>>> GetInvoices()
    {
        var invoices = await service.GetInvoices();
        return OkResponse(invoices);
    }

    [HttpGet("{number:long}")]
    public async Task<ActionResult<Response<InvoiceDto>>> GetInvoice(long number)
    {
        var invoice = await service.GetInvoice(number);
        return OkResponse(invoice);
    }

    [HttpGet("company/{cnpj}")]
    public async Task<ActionResult<Response<List<InvoiceDto>>>> GetInvoicesByCompany(string cnpj)
    {
        var invoices = await service.GetInvoicesByCompany(cnpj);
        return OkResponse(invoices);
    }

    [HttpPost]
    public async Task<ActionResult<Response<InvoiceDto>>> PostInvoice([FromBody] AddInvoiceDto invoice)
    {
        var newInvoice = await service.PostInvoice(invoice);
        return OkResponse(newInvoice);
    }

    [HttpDelete("{number:long}")]
    public async Task<ActionResult<Response>> DeleteInvoice(long number)
    {
        await service.DeleteInvoice(number);
        return OkResponse();
    }
}