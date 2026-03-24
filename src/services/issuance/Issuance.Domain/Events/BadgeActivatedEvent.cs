namespace Issuance.Domain.Events;

public class BadgeActivatedEvent : IDomainEvent
{
    public Guid AssertionId { get; }
    public Guid BadgeClassId { get; }
    public string HashedEmail { get; }
    public DateTime IssuedOn { get; }
    public BadgeActivatedEvent(Guid assertionId, Guid badgeClassId, string hashedEmail, DateTime issuedOn)
    {
        AssertionId = assertionId;
        BadgeClassId = badgeClassId;
        HashedEmail = hashedEmail;
        IssuedOn = issuedOn;
    }
}