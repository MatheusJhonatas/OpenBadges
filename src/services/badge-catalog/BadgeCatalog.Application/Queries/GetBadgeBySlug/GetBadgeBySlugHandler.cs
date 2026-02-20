using BadgeCatalog.Domain.Aggregates;
using BadgeCatalog.Ports.Repositories;

namespace BadgeCatalog.Application.Queries.GetBadgeBySlug;

public sealed class GetBadgeBySlugHandler
{
    private readonly IBadgeClassRepository  _repository;

    public GetBadgeBySlugHandler(IBadgeClassRepository repository)
    {
        _repository = repository;
    }

    public Task<BadgeClass?> Handle(string slug, CancellationToken cancellationToken)
    {
        return _repository.GetBySlugAsync(slug, cancellationToken);
    }
}