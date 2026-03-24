using Issuance.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Issuance.Application.EventsHandlers.BadgeIssued;

// Handler que reage ao evento de badge emitido
public class BadgeIssuedEventHandler : INotificationHandler<BadgeIssuedEvent>
{
    private readonly ILogger<BadgeIssuedEventHandler> _logger;
    public BadgeIssuedEventHandler(ILogger<BadgeIssuedEventHandler> logger)
    {
        _logger = logger;
    }
    public Task Handle(BadgeIssuedEvent notification, CancellationToken cancellationToken)
    {
        // 🔥 Simulação de ação (ex: log, email, integração)
        _logger.LogInformation("Handler ORIGINAL executado");

        return Task.CompletedTask;
    }
}