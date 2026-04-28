using BadgeCatalog.Domain.Exceptions;
using BadgeCatalog.Ports.Repositories;
using MediatR;

namespace BadgeCatalog.Application.Commands.UpdateBadgeClass;

public class UpdateBadgeClassHandler : IRequestHandler<UpdateBadgeClassCommand, bool>
{
    private readonly IBadgeClassRepository _repository;
    public UpdateBadgeClassHandler(IBadgeClassRepository repository)
    {
        _repository = repository;
    }
    public async Task<bool> Handle(UpdateBadgeClassCommand command, CancellationToken cancellationToken)
    {
        var badge = await _repository.GetByIdAsync(command.Id, cancellationToken);

        if (badge == null)
            return false;

           if(badge.Version != command.Version)
        {
            throw new ConcurrencyException(
    "The badge has been modified by another process. Please reload and try again.");
        }
        badge.Update(command.Name, command.Description, command.CriteriaNarrative, command.TemplateId);
        await _repository.UpdateAsync(badge, cancellationToken);
        return true;
    }
}