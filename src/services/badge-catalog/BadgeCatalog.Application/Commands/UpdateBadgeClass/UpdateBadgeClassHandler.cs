using BadgeCatalog.Ports.Repositories;

namespace BadgeCatalog.Application.Commands.UpdateBadgeClass;

public class UpdateBadgeClassHandler
{
    private readonly IBadgeClassRepository _repository;
    public UpdateBadgeClassHandler(IBadgeClassRepository repository)
    {
        _repository = repository;
    }
        
    public async Task<bool> Handle(
        Guid id,
        UpdateBadgeClassCommand command,
        CancellationToken cancellationToken
    )
    {
        var badge = await _repository.GetByIdAsync(id, cancellationToken);

        if (badge == null)
            return false;

           if(badge.Version != command.Version)
            {
                throw new InvalidOperationException("The badge has been modified by another process. Please reload and try again.");
            }
        badge.Update(command.Name, command.Description);
        await _repository.UpdateAsync(badge, cancellationToken);
        return true;
    }
}