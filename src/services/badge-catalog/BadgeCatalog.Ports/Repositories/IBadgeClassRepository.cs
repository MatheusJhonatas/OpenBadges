
using BadgeCatalog.Domain.Aggregates;
using BadgeCatalog.Domain.Specifications;

namespace BadgeCatalog.Ports.Repositories;

public interface IBadgeClassRepository
{
    Task AddAsync(BadgeClass badgeClass, CancellationToken cancellationToken);
    Task<IReadOnlyList<BadgeClass>> GetAllAsync(CancellationToken cancellationToken);
    Task<IReadOnlyList<BadgeClass>> ListAsync(Specification<BadgeClass> specification, CancellationToken cancellationToken);
    Task<BadgeClass?> GetBySlugAsync(string slug, CancellationToken cancellationToken);
}