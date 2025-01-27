using Microsoft.EntityFrameworkCore;
using AsteriaChallenger.Models;

namespace AsteriaChallenger.Data
{
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<SalesData> Sales { get; set; } // Tabela de vendas

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      // Configurações extras, se necessário
    }
  }
}