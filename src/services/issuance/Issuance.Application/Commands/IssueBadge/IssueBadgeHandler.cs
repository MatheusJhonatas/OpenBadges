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
    private readonly IMediator _mediator;
    
    public IssuanceHandler(IAssertionRepository repository, IBadgeCatalogClient badgeCatalogClient, IMediator mediator)
    {
        _repository = repository;
        _badgeCatalogClient = badgeCatalogClient;
        _mediator = mediator;
    }

    public async Task<Guid> Handle(IssueBadgeCommand command, CancellationToken cancellationToken)
    {
        var badgeExists = await _badgeCatalogClient.BadgeExistsAsync(command.BadgeClassId, cancellationToken);
        
        if (!badgeExists)
        {
            throw new BadgeNotFoundException(command.BadgeClassId);
        }

        var alreadyExists = await _repository.ExistsAsync(
            command.BadgeClassId, 
            RecipientIdentity.GenerateHash(command.RecipientEmail), 
            cancellationToken   
        );
        
        if (alreadyExists)
        {
            throw new DuplicateAssertionException(command.BadgeClassId, command.RecipientEmail);
        }

        var recipient = RecipientIdentity.Create(command.RecipientEmail);

        var assertion = new Assertion(command.BadgeClassId, recipient);

        await _repository.AddAsync(assertion, cancellationToken);
        // 🔥 Captura eventos de domínio
        var events = assertion.DomainEvents;
        //pública os Domain Events usando o MediatR
        foreach(var domainEvent in assertion.DomainEvents)
        {
            await _mediator.Publish(domainEvent, cancellationToken);
        }
        // limpa eventos depois de usar
        assertion.ClearDomainEvents();
        
        return assertion.Id;

        
    }
}