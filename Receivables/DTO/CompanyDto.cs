namespace Receivables.DTO.Outgoing;

public class CompanyDto
{
    public string cnpj { get; set; } = string.Empty;
    public string name { get; set; } = string.Empty;
    public double monthlyBilling { get; set; }
    public string sector { get; set; } = string.Empty;
    public List<InvoiceDto> invoices { get; set; } = [];
}