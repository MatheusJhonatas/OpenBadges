using Issuance.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Issuance.Application.EventsHandlers.BadgeIssued;

public class SendBadgeIssuedEmailHandler : INotificationHandler<BadgeIssuedEvent>
{
    private readonly ILogger<SendBadgeIssuedEmailHandler> _logger;

    public SendBadgeIssuedEmailHandler(ILogger<SendBadgeIssuedEmailHandler> logger)
    {
        _logger = logger;
    }
    public Task Handle(BadgeIssuedEvent notification, CancellationToken cancellationToken)
    {
        // Simulação de envio de email
        _logger.LogInformation("Email handler executado");

        return Task.CompletedTask;
    }
}