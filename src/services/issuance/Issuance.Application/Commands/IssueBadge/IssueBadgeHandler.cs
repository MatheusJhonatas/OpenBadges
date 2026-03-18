using Issuance.Domain.Aggregates;
using Issuance.Domain.Exceptions;
using Issuance.Domain.ValueObjects;
using Issuance.Ports.Clients;
using Issuance.Ports.Repositories;
using MediatR;

namespace Issuance.Application.Commands.IssueBadge;

public sealed class IssuanceHandler : IRequestHandler<IssueBadgeCommand, Guid>
{
    private readonly IAssertionRepository _repository;
    private readonly IBadgeCatalogClient _badgeCatalogClient;
    
    public IssuanceHandler(IAssertionRepository repository, IBadgeCatalogClient badgeCatalogClient)
    {
        _repository = repository;
        _badgeCatalogClient = badgeCatalogClient;
    }

    public async Task<Guid> Handle(IssueBadgeCommand command, CancellationToken cancellationToken)
    {
        var badgeExists = await _badgeCatalogClient.BadgeExistsAsync(command.BadgeClassId, cancellationToken);
        
        if (!badgeExists)
        {
            throw new BadgeNotFoundException(command.BadgeClassId);
        }
        
        var recipient = RecipientIdentity.Create(command.RecipientEmail);

        var asserttion = new Assertion(command.BadgeClassId, recipient); 

        await _repository.AddAsync(asserttion, cancellationToken);
        
        return asserttion.Id;
    }
}