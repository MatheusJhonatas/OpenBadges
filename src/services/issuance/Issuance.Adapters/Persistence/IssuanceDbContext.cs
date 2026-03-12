using Issuance.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Issuance.Adapters.Persistence;
public class IssuanceDbContext : DbContext
{
    public IssuanceDbContext(DbContextOptions<IssuanceDbContext> options) : base(options)
    {
    }

    //Declarando DbSet para cada entidade do domínio
    public DbSet<Assertion> Assertions => Set<Assertion>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IssuanceDbContext).Assembly);

        // Configurações adicionais de mapeamento podem ser feitas aqui
        // Exemplo: modelBuilder.Entity<Assertion>().ToTable("Assertions");
    }
}