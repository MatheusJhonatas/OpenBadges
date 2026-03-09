using BadgeCatalog.Ports.Repositories;
using MediatR;

namespace BadgeCatalog.Application.Commands.ActiveBadgeClass;

public class ActiveBadgeClassHandler : IRequestHandler<ActiveBadgeClassCommand, bool>
{
    //Injetando o repositório para acessar os dados das badge 
    // classes e realizar as operações necessárias para ativar uma badge class.
    private readonly IBadgeClassRepository _repository;
    public ActiveBadgeClassHandler(IBadgeClassRepository repository)
    {
        _repository = repository;
    }
    public async Task<bool> Handle(ActiveBadgeClassCommand command, CancellationToken cancellationToken)
    {
        var badge = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if(badge == null)
            return false;

        badge.Activate();

        await _repository.UpdateAsync(badge, cancellationToken);
        
        return true;
    }
}