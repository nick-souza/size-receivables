namespace Receivables.DTO.Outgoing;

public class ReceivableDto
{
    public string company { get; set; } = string.Empty;

    public string cnpj { get; set; } = string.Empty;

    public double limit { get; set; }

    // Total Líquido
    public double net_total { get; set; }

    // Total Bruto
    public double gross_total { get; set; }

    public List<ReceivableInvoiceDto> invoices { get; set; } = [];
}

public class ReceivableInvoiceDto
{
    public long number { get; set; }

    // Valor Bruto
    public double net_total { get; set; }

    // Valor Líquido
    public double gross_total { get; set; }
}