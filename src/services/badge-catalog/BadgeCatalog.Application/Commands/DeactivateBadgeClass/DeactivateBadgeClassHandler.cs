using BadgeCatalog.Ports.Repositories;

namespace BadgeCatalog.Application.Commands.DeactivateBadgeClass;

public class DeactivateBadgeClassHandler
{
    private readonly IBadgeClassRepository _repository;

    public DeactivateBadgeClassHandler(IBadgeClassRepository repository)
    {
        _repository = repository;
    }

       public async Task<bool> Handle(Guid id, CancellationToken cancellationToken)
    {
        var badge = await _repository.GetByIdAsync(id, cancellationToken);

        if (badge is null)
            return false;

        badge.Deactivate();

        await _repository.UpdateAsync(badge, cancellationToken);

        return true;
    }
}