using BadgeCatalog.Domain.Aggregates;
using BadgeCatalog.Ports.Repositories;
using MediatR;

namespace BadgeCatalog.Application.Queries.GetBadgeById;

public class GetBadgeByIdHandler : IRequestHandler<GetBadgeByIdQuery, BadgeClass?>
{
    private readonly IBadgeClassRepository _repository;

    public GetBadgeByIdHandler(IBadgeClassRepository repository)
    {
        _repository = repository;
    }

    public async Task<BadgeClass> Handle(GetBadgeByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.Id, cancellationToken);
    }
}