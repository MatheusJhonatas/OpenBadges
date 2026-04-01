namespace Issuance.Domain.Events;

public class BadgeIssuedEvent : IDomainEvent
{
    public Guid AssertionId { get; }
    public Guid BadgeClassId { get; }
    public string HashedEmail { get; }
    public string RecipientName { get; }
    public DateTime IssuedOn { get; }

    public BadgeIssuedEvent(Guid assertionId, Guid badgeClassId, string recipientEmail, string recipientName, DateTime issuedOn)
    {
        AssertionId = assertionId;
        BadgeClassId = badgeClassId;
        HashedEmail = recipientEmail;
        RecipientName = recipientName;
        IssuedOn = issuedOn;
    }
}