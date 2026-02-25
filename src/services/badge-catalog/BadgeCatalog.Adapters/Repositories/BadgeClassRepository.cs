using BadgeCatalog.Adapters.Persistence;
using BadgeCatalog.Domain.Aggregates;
using BadgeCatalog.Domain.Specifications;
using BadgeCatalog.Ports.Repositories;

namespace BadgeCatalog.Adapters.Repositories;

public class BadgeClassRepository : IBadgeClassRepository
{
    private readonly BadgeCatalogDbContext _dbContext;

    public BadgeClassRepository(BadgeCatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(BadgeClass badgeClass, CancellationToken cancellationToken)
    {
        await _dbContext.BadgeClasses.AddAsync(badgeClass, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<IReadOnlyList<BadgeClass>> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<BadgeClass>> ListAsync(Specification<BadgeClass> specification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<BadgeClass?> GetBySlugAsync(string slug, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    Task<BadgeClass?> IBadgeClassRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(BadgeClass badgeClass, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}