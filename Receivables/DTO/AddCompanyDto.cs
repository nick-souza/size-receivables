using Receivables.Entities;

namespace Receivables.DTO.Outgoing;

public class AddCompanyDto
{
    public string cnpj { get; set; } = string.Empty;
    public string name { get; set; } = string.Empty;
    public double? monthlyBilling { get; set; }
    public Sector? sector { get; set; }
}