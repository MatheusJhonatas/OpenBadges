using MediatR;

namespace BadgeCatalog.Application.Commands.ActiveBadgeClass;

public sealed record ActiveBadgeClassCommand(Guid Id) : IRequest<bool>;