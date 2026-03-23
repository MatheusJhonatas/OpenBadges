using Issuance.Domain.Events;
using MediatR;

namespace Issuance.Application.Events.Handlers;

// Handler que reage quando um badge é revogado
public class BadgeRevokedEventHandler : INotificationHandler<BadgeRevokedEvent>
{
    public Task Handle(BadgeRevokedEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"[EVENT] Badge revoked for {notification.HashedEmail }");

        return Task.CompletedTask;
    }
}