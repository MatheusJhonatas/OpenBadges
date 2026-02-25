using BadgeCatalog.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace BadgeCatalog.Adapters.Persistence;

public class BadgeCatalogDbContext : DbContext
{
    public BadgeCatalogDbContext(DbContextOptions<BadgeCatalogDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BadgeCatalogDbContext).Assembly);
    }
    public DbSet<BadgeClass> BadgeClasses => Set<BadgeClass>();
}


