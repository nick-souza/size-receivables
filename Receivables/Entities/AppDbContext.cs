namespace Receivables.Entities;

using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }

    public AppDbContext() : base() { }

    public DbSet<Company> Company { get; set; }
    public DbSet<Invoice> Invoice { get; set; }
}