
using MediatR;

namespace BadgeCatalog.Application.Commands.DeactivateBadgeClass;
//classe selada pois não tem necessidade de ser herdada, 
//e é imutável, ou seja, seus valores não podem ser alterados após a criação do objeto.
public sealed record DeactivateBadgeClassCommand(Guid Id) : IRequest<bool>;