using MediatR;

namespace BadgeCatalog.Application.Commands.UpdateBadgeClass;
//classe selada pois não tem necessidade de ser herdada, 
//e é imutável, ou seja, seus valores não podem ser alterados após a criação do objeto.
public sealed record UpdateBadgeClassCommand(Guid Id) : IRequest<bool>
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int Version { get; set; }
}