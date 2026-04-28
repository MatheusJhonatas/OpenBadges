using MediatR;

namespace BadgeCatalog.Application.Commands.CreateBadgeClass;
//classe selada pois não tem necessidade de ser herdada, 
//e é imutável, ou seja, seus valores não podem ser alterados após a criação do objeto.
public sealed record CreateBadgeClassCommand(
    string Name, 
    string Description, 
    string TemplateId, 
    string CriteriaNarrative) : IRequest<Guid>
{
    
}