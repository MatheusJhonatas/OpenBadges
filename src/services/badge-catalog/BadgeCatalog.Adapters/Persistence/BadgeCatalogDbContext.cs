using Microsoft.EntityFrameworkCore;

namespace BadgeCatalog.Adapters.Persistence;

public class BadgeCatalogDbContext : DbContext
{
    public BadgeCatalogDbContext(DbContextOptions<BadgeCatalogDbContext> options) : base(options)
    {
    }


}


