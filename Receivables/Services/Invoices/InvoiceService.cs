using API.Services.Errors;
using Receivables.DTO.Outgoing;
using Receivables.Entities;
using Receivables.Repositories;
using Receivables.Services.Utils;

namespace Receivables.Services.Invoices;

public class InvoiceService : IInvoiceService
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly ICompanyRepository _companyRepository;

    public InvoiceService(IInvoiceRepository invoiceRepository, ICompanyRepository companyRepository)
    {
        _invoiceRepository = invoiceRepository;
        _companyRepository = companyRepository;
    }

    public async Task<List<InvoiceDto>> GetInvoices()
    {
        var result = await _invoiceRepository.SelectAsync();
        return result.Select(Mapper.MapToInvoiceDto).ToList();
    }

    public async Task<InvoiceDto> GetInvoice(long number)
    {
        if (number <= 0) throw new BadRequestException("Invalid invoice number");

        var result = await _invoiceRepository.GetByIdAsync(number);
        if (result == null) throw new RecordNotFoundException("Invoice not found");

        return Mapper.MapToInvoiceDto(result);
    }

    public async Task<List<InvoiceDto>> GetInvoicesByCompany(string cnpj)
    {
        if (string.IsNullOrWhiteSpace(cnpj)) throw new BadRequestException("Invalid CNPJ");

        var result = await _invoiceRepository.SelectByCompanyIdAsync(cnpj);
        return result.Select(Mapper.MapToInvoiceDto).ToList();
    }

    public async Task<InvoiceDto> PostInvoice(AddInvoiceDto model)
    {
        if (!ValidateModel(model)) throw new BadRequestException("Invalid model");

        var company = await _companyRepository.GetByIdAsync(model.cnpj);
        if (company == null) throw new BadRequestException("Company not found");

        var prevInvoice = await _invoiceRepository.GetByIdAsync(model.number);
        if (prevInvoice != null) throw new BadRequestException("Invoice already exists");

        var dueDate = DateTime.ParseExact(model.dueDate, "dd/MM/yyyy", null);
        if (dueDate < DateTime.Now.Date) throw new BadRequestException("Due date must be in the future");

        var invoice = new Invoice
        {
            Number = model.number,
            Cnpj = model.cnpj,
            Value = model.value,
            DueDate = dueDate
        };

        await _invoiceRepository.AddAsync(invoice);

        return Mapper.MapToInvoiceDto(invoice);
    }

    public async Task DeleteInvoice(long number)
    {
        if (number <= 0) throw new BadRequestException("Invalid invoice number");

        var invoice = await _invoiceRepository.GetByIdAsync(number);
        if (invoice == null) throw new RecordNotFoundException("Invoice not found");

        await _invoiceRepository.DeleteAsync(invoice);
    }

    private bool ValidateModel(AddInvoiceDto model)
    {
        if (model.number <= 0) return false;
        if (model.value <= 0) return false;

        if (string.IsNullOrWhiteSpace(model.cnpj)) return false;
        if (string.IsNullOrWhiteSpace(model.dueDate)) return false;

        return true;
    }
}