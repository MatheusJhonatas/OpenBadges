namespace Issuance.Domain.Events;

public class BadgeIssuedEvent
{
    public Guid AssertionId { get; }
    public Guid BadgeClassId { get; }
    public string RecipientEmail { get; }
    public DateTime IssuedOn { get; }

    public BadgeIssuedEvent(Guid assertionId, Guid badgeClassId, string recipientEmail, DateTime issuedOn)
    {
        AssertionId = assertionId;
        BadgeClassId = badgeClassId;
        RecipientEmail = recipientEmail;
        IssuedOn = issuedOn;
    }
}