using Issuance.Domain.Events;
using MediatR;

namespace Issuance.Application.EventsHandlers.BadgeIssued;

public class SendBadgeIssuedEmailHandler : INotificationHandler<BadgeIssuedEvent>
{
    public Task Handle(BadgeIssuedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}