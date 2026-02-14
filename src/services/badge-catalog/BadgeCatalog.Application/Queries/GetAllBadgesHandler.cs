using BadgeCatalog.Domain.Aggregates;
using BadgeCatalog.Ports;
using BadgeCatalog.Ports.Repositories;

namespace BadgeCatalog.Application.Queries.GetAllBadges;

public sealed class GetAllBadgesHandler
{
    private readonly IBadgeClassRepository _repository;

    public GetAllBadgesHandler(IBadgeClassRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyList<BadgeClass>> Handle(CancellationToken cancellationToken)
    {
        return _repository.GetAllAsync(cancellationToken);
    }
}
