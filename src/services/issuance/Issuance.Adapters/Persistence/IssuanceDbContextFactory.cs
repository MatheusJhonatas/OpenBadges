using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Issuance.Adapters.Persistence;

public class IssuanceDbContextFactory : IDesignTimeDbContextFactory<IssuanceDbContext>
{
    public IssuanceDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<IssuanceDbContext>();

        optionsBuilder.UseSqlite("Data Source=issuance.db");

        return new IssuanceDbContext(optionsBuilder.Options);
    }
}