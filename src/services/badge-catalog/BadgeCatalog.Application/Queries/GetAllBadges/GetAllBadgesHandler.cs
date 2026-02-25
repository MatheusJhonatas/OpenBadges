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
        if (active.HasValue)
        {
            if (active.Value)
            {
                var spec = new ActiveBadgeSpecification();
                return await _repository.ListAsync(spec, cancellationToken);
            }
            else
            {
                var spec = new InactiveBadgeSpecification();
            return await _repository.ListAsync(spec, cancellationToken);
            }
        }
        // DEFAULT: só ativos
    var defaultSpec = new ActiveBadgeSpecification();
    return await _repository.ListAsync(defaultSpec, cancellationToken);
}

}
