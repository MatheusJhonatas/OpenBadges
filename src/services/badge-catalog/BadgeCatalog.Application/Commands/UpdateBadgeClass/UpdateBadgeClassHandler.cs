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
        string name,
        string description,
        CancellationToken cancellationToken
    )
    {
        var badge = await _repository.GetByIdAsync(id, cancellationToken);
        if (badge == null)
            return false;
        badge.Update(name, description);
        await _repository.UpdateAsync(badge, cancellationToken);
        return true;
    }
}