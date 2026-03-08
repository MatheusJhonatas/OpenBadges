using BadgeCatalog.Ports.Repositories;
using MediatR;

namespace BadgeCatalog.Application.Commands.DeactivateBadgeClass;

public class DeactivateBadgeClassHandler : IRequestHandler<DeactivateBadgeClassCommand, bool>
{
    private readonly IBadgeClassRepository _repository;

    public DeactivateBadgeClassHandler(IBadgeClassRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeactivateBadgeClassCommand command, CancellationToken cancellationToken)
    {
        var badge = await _repository.GetByIdAsync(command.Id, cancellationToken);

        if (badge is null)
            return false;

        badge.Deactivate();

        await _repository.UpdateAsync(badge, cancellationToken);

        return true;
    }

}