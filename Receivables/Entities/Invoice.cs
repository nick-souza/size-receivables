using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Receivables.Entities;

[Table("tb_invoices")]
public class Invoice
{
    [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public long Number { get; set; }

    [Required]
    public double Value { get; set; }

    [Required]
    public DateTime DueDate { get; set; }

    [Required, MaxLength(20), ForeignKey("Company")]
    public string Cnpj { get; set; } = string.Empty;

    public virtual Company Company { get; set; } = null!;
}