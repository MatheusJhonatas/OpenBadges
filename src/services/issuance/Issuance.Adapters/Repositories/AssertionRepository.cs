using Issuance.Adapters.Persistence;
using Issuance.Domain.Aggregates;
using Issuance.Ports.Repositories;

namespace Issuance.Adapters.Repositories;


public class AssertionRepository : IAssertionRepository
{
    private readonly IssuanceDbContext _dbContext;
    public AssertionRepository(IssuanceDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task AddAsync(Assertion assertion, CancellationToken cancellationToken)
    {
        await _dbContext.Assertions.AddAsync(assertion, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}

