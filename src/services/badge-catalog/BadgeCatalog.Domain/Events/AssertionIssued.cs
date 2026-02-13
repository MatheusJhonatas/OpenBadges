namespace BadgeCatalog.Domain.Events;

public sealed class AssertionIssued
{
    public Guid AssertionId { get; }
    public Guid BadgeClassId { get; }
    public DateTime IssuedOn { get; }

    public AssertionIssued(Guid assertionId, Guid badgeClassId, DateTime issuedOn)
    {
        AssertionId = assertionId;
        BadgeClassId = badgeClassId;
        IssuedOn = issuedOn;
    }
}