using BadgeCatalog.Domain.Aggregates;
using BadgeCatalog.Domain.ValueObjects;
using BadgeCatalog.Ports.Repositories;
namespace BadgeCatalog.Application.Commands.CreateBadgeClass;

public sealed class CreateBadgeClassHandler
{
    private readonly IBadgeClassRepository _repository;
    public CreateBadgeClassHandler(IBadgeClassRepository repository)
    {
        _repository = repository;
    }
    public async Task<BadgeClass> Handle(CreateBadgeClassCommand command, CancellationToken cancellationToken)
    {
        var image = new BadgeImage(command.ImageUrl);
        var criteria = new BadgeCriteria(command.CriteriaNarrative);
        var badgeClass = new BadgeClass(command.Name, 
        command.Description, 
        image, 
        criteria);
        await _repository.AddAsync(badgeClass, cancellationToken);
        return badgeClass;
    }    
    
}