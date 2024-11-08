namespace Receivables.Entities;

using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }

    public AppDbContext() : base() { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Invoice>()
            .HasOne(i => i.Company)
            .WithMany(c => c.Invoices)
            .HasForeignKey(i => i.Cnpj);
    }

    public DbSet<Company> Company { get; set; }
    public DbSet<Invoice> Invoice { get; set; }
}