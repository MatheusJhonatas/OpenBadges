namespace Issuance.Domain.Events;

public class BadgeRevokedEvent : IDomainEvent
{
    public Guid AssertionId { get; }
    public Guid BadgeClassId { get; }
    public string HashedEmail { get; }
    public DateTime RevokedOn { get; }
    public BadgeRevokedEvent(Guid assertionId, Guid badgeClassId, string hashedEmail, DateTime revokedOn)
    {
        AssertionId = assertionId;
        BadgeClassId = badgeClassId;
        HashedEmail = hashedEmail;
        RevokedOn = revokedOn;
    }
}