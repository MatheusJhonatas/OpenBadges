using BadgeCatalog.Adapters.Persistence;
using BadgeCatalog.Domain.Aggregates;
using BadgeCatalog.Domain.Specifications;
using BadgeCatalog.Ports.Repositories;
using Microsoft.EntityFrameworkCore;

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

    public async Task<IReadOnlyList<BadgeClass>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.BadgeClasses
        .AsNoTracking()
        .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<BadgeClass>> ListAsync(Specification<BadgeClass> specification, CancellationToken cancellationToken)
    {
        var query = _dbContext.BadgeClasses
        .AsNoTracking()
        .Where(specification.ToExpression());
        return await query.ToListAsync(cancellationToken);
    }

    public async Task<BadgeClass?> GetBySlugAsync(string slug, CancellationToken cancellationToken)
    {
        return await _dbContext.BadgeClasses
        .AsNoTracking()
        .FirstOrDefaultAsync(bc => bc.Slug == slug, cancellationToken);
    }

    public async Task<BadgeClass?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.BadgeClasses
        .FirstOrDefaultAsync(bc => bc.Id == id, cancellationToken);
    }

    public async Task UpdateAsync(BadgeClass badgeClass, CancellationToken cancellationToken)
    {
       _dbContext.BadgeClasses.Update(badgeClass);
         await _dbContext.SaveChangesAsync(cancellationToken);
    }
}