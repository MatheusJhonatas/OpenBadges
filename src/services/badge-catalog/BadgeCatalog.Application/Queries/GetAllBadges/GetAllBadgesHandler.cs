using BadgeCatalog.Domain.Aggregates;
using BadgeCatalog.Domain.Specifications;
using BadgeCatalog.Ports.Repositories;
using MediatR;

namespace BadgeCatalog.Application.Queries.GetAllBadges;

public sealed class GetAllBadgesHandler 
    : IRequestHandler<GetAllBadgesQuery, IReadOnlyList<BadgeClass>>
{
    private readonly IBadgeClassRepository _repository;

    public GetAllBadgesHandler(IBadgeClassRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<BadgeClass>> Handle(
        GetAllBadgesQuery query,
        CancellationToken cancellationToken)
    {
        if (query.Active.HasValue)
        {
            if (query.Active.Value)
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