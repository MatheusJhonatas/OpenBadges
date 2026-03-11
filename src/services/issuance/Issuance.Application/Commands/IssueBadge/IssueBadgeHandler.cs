using Issuance.Domain.Aggregates;
using Issuance.Domain.ValueObjects;
using MediatR;

namespace Issuance.Application.Commands.IssueBadge;

public sealed class IssuanceHandler : IRequestHandler<IssueBadgeCommand, Guid>
{
    public Task<Guid> Handle(IssueBadgeCommand command, CancellationToken cancellationToken)
    {
        var recipient = new RecipientIdentity(command.RecipientEmail);

        var asserttion = new Assertion(command.BadgeClassId, recipient); 

        return Task.FromResult(asserttion.Id);
    }
}