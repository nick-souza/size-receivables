namespace Receivables.DTO.Outgoing;

public class AddInvoiceDto
{
    public long number { get; set; }
    public double value { get; set; }
    public string dueDate { get; set; } = string.Empty;
    public string cnpj { get; set; } = string.Empty;
}