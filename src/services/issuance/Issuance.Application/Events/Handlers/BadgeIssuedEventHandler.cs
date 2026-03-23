using Issuance.Domain.Events;
using MediatR;

namespace Issuance.Application.Events.Handlers;

// Handler que reage ao evento de badge emitido
public class BadgeIssuedEventHandler : INotificationHandler<BadgeIssuedEvent>
{
    public Task Handle(BadgeIssuedEvent notification, CancellationToken cancellationToken)
    {
        // 🔥 Simulação de ação (ex: log, email, integração)
        Console.WriteLine($"[EVENT] Badge issued for {notification.RecipientEmail }");

        return Task.CompletedTask;
    }
}