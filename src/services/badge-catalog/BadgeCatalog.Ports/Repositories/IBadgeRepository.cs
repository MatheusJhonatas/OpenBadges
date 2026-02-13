
using BadgeCatalog.Domain.Aggregates;

namespace BadgeCatalog.Ports.Repositories;

public interface IBadgeClassRepository
{
    Task AddAsync(BadgeClass badgeClass, CancellationToken cancellationToken);
}