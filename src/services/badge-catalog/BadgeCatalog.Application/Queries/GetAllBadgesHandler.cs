using BadgeCatalog.Domain.Aggregates;
using BadgeCatalog.Domain.Specifications;
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

    public async Task<IReadOnlyList<BadgeClass>> Handle(
    bool? active,
    CancellationToken cancellationToken)
{
    if (active == true)
    {
        var spec = new ActiveBadgeSpecification();
        return await _repository.ListAsync(spec, cancellationToken);
    }

    return await _repository.GetAllAsync(cancellationToken);
}

}
