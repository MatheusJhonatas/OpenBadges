using BadgeCatalog.Domain.Aggregates;
using MediatR;

public record GetBadgeByIdQuery(Guid Id) : IRequest<BadgeClass?>;