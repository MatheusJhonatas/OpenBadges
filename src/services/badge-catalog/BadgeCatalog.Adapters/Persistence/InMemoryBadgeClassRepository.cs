using BadgeCatalog.Domain.Aggregates;
using BadgeCatalog.Domain.Specifications;
using BadgeCatalog.Ports.Repositories;

namespace BadgeCatalog.Adapters.Persistence;

public sealed class InMemoryBadgeClassRepository : IBadgeClassRepository
{
    private readonly List<BadgeClass> _storage = new();

    public Task AddAsync(BadgeClass badgeClass, CancellationToken cancellationToken)
    {
        _storage.Add(badgeClass);
        return Task.CompletedTask;
    }
    public Task<BadgeClass?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var badgeClass = _storage.FirstOrDefault(b => b.Id == id);
        return Task.FromResult(badgeClass);
    }
    public Task<IReadOnlyList<BadgeClass>> GetAllAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult((IReadOnlyList<BadgeClass>)_storage);
    }

    public Task UpdateAsync(BadgeClass badgeClass, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task<IReadOnlyList<BadgeClass>> ListAsync(Specification<BadgeClass> specification, CancellationToken cancellationToken)
    {
        var result = _storage
        .Where(specification.ToFunc())
        .ToList();
        return Task.FromResult((IReadOnlyList<BadgeClass>)result);
    }
}