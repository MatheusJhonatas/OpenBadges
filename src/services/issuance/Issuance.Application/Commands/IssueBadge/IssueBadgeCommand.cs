using MediatR;
namespace Issuance.Application.Commands.IssueBadge;

public sealed record IssueBadgeCommand : IRequest<Guid>
{
    public Guid BadgeClassId { get; init; }
    public string RecipientEmail { get; init; } = default!;
    public string RecipientName { get; init; } = default!;
}