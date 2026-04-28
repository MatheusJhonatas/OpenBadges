using BadgeCatalog.Domain.Aggregates;
using BadgeCatalog.Domain.ValueObjects;
using BadgeCatalog.Ports.Repositories;
using MediatR;
namespace BadgeCatalog.Application.Commands.CreateBadgeClass;

public sealed class CreateBadgeClassHandler : IRequestHandler<CreateBadgeClassCommand, Guid>
{
    private readonly IBadgeClassRepository _repository;
    public CreateBadgeClassHandler(IBadgeClassRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Guid> Handle(CreateBadgeClassCommand command, CancellationToken cancellationToken)
    {
        var templateId = new BadgeTemplateId(command.TemplateId);
        var criteria = new BadgeCriteria(command.CriteriaNarrative);

        var badgeClass = new BadgeClass(
            command.Name,
            command.Description,
            templateId,
            criteria
        );

        await _repository.AddAsync(badgeClass, cancellationToken);

        return badgeClass.Id;
    }
}