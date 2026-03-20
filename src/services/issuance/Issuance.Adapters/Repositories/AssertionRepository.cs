using Issuance.Adapters.Persistence;
using Issuance.Domain.Aggregates;
using Issuance.Domain.ValueObjects;
using Issuance.Ports.Repositories;
using Microsoft.EntityFrameworkCore;

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

    public async Task<bool> ExistsAsync(Guid badgeId, string email, CancellationToken cancellationToken)
    {
        var hashedEmail = RecipientIdentity.GenerateHash(email);
        return await _dbContext.Assertions.AnyAsync(a => a.BadgeClassId == badgeId && a.Recipient.HashedEmail == email, cancellationToken);
    }

    public async Task<Assertion?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Assertions.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task UpdateAsync(Assertion assertion, CancellationToken cancellationToken)
    {
        _dbContext.Assertions.Update(assertion);
        await _dbContext.SaveChangesAsync(cancellationToken);   
    }
}

