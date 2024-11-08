using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Receivables.Entities;

[Table("tb_companies")]
public class Company
{
    [Key, Required, MaxLength(20), DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string Cnpj { get; set; } = string.Empty;

    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public double MonthlyBilling { get; set; }

    [Required]
    public Sector Sector { get; set; } = Sector.Service;

    public virtual List<Invoice> Invoices { get; set; } = [];
}