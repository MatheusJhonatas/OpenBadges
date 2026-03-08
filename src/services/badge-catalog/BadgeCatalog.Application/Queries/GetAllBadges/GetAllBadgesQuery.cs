using BadgeCatalog.Domain.Aggregates;
using MediatR;

namespace BadgeCatalog.Application.Queries.GetAllBadges;

public sealed record GetAllBadgesQuery(bool? Active) : IRequest<IReadOnlyList<BadgeClass>>;
