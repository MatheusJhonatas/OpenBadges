using Issuance.Domain.Aggregates;
using Issuance.Domain.ValueObjects;
using Issuance.Ports.Repositories;
using MediatR;

namespace Issuance.Application.Commands.IssueBadge;

public sealed class IssuanceHandler : IRequestHandler<IssueBadgeCommand, Guid>
{
    private readonly IAssertionRepository _repository;
    
    public IssuanceHandler(IAssertionRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(IssueBadgeCommand command, CancellationToken cancellationToken)
    {
        var recipient = new RecipientIdentity(command.RecipientEmail);

        var asserttion = new Assertion(command.BadgeClassId, recipient); 

        await _repository.AddAsync(asserttion, cancellationToken);
        
        return asserttion.Id;
    }
}