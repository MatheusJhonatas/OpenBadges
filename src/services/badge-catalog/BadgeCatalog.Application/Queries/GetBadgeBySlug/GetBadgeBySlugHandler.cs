using BadgeCatalog.Domain.Aggregates;
using BadgeCatalog.Ports.Repositories;
using MediatR;

namespace BadgeCatalog.Application.Queries.GetBadgeBySlug;

public sealed class GetBadgeBySlugHandler : IRequestHandler<GetBadgeBySlugQuery, BadgeClass?>
{
    private readonly IBadgeClassRepository  _repository;

    public GetBadgeBySlugHandler(IBadgeClassRepository repository)
    {
        _repository = repository;
    }

    

    public async Task<BadgeClass?> Handle(GetBadgeBySlugQuery query, CancellationToken cancellationToken)
    {
        return await _repository.GetBySlugAsync(query.Slug, cancellationToken);
    }
}