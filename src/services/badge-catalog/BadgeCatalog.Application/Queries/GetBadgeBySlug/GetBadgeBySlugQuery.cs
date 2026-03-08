using BadgeCatalog.Domain.Aggregates;
using MediatR;

namespace BadgeCatalog.Application.Queries.GetBadgeBySlug;

public sealed record GetBadgeBySlugQuery(string Slug) : IRequest<BadgeClass>;